using PisiMobile.BusinessCore.Integration;
using PisiMobile.BusinessCore.Interface;
using PisiMobile.CoreObject;
using PisiMobile.CoreObject.DataTransferObject.Request;
using PisiMobile.CoreObject.DataTransferObject.Response;
using PisiMobile.CoreObject.Helpers;
using PisiMobile.CoreObject.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PisiMobile.BusinessCore.Service
{
    public class UserAuthentication : IUserAuthentication
    {
        private readonly AppSettings _appSettings;
        private DatabaseContext _appDbContext;
        private IPasswordManagement _passwordManagement;

        public UserAuthentication(IOptions<AppSettings> appSettings, DatabaseContext appDbContext, IPasswordManagement passwordManagement)
        {
            _appSettings = appSettings.Value;
            _appDbContext = appDbContext;   
            _passwordManagement = passwordManagement;
        }

        public async Task<IEnumerable<UserSubscriptionResponse>> GetActiveUserSubscriptions()
        {
            //var ListOfUsers = await _appDbContext.Users.ToListAsync();

            return (from user in _appDbContext.Users
                      join sub in _appDbContext.Subscriptions on user.Id equals sub.UserId
                      join service in _appDbContext.PisiServices on sub.ServiceId equals service.Id
                      where user.IsActive == CoreObject.Enum.StatusEnum.Active
                      select new 
                      {
                          PhoneNumber = user.PhoneNumber,
                          Address = user.Address,
                          Age = user.Age,
                          FirstName = user.FirstName,
                          LastName = user.LastName,
                          MiddleName = user.MiddleName,
                          Nationality = user.Nationality,
                          Service = service.Name,
                          SubscriptionStatus = sub.Status,
                          SubscriptionDate = sub.UpdatedDate
                      }).GroupBy(x => x.PhoneNumber).Select(x => new UserSubscriptionResponse
                      {
                          PhoneNumber = x.Key,
                          Address = x.First().Address ?? string.Empty,
                          Age = x.First().Age ?? null,
                          FirstName = x.First().FirstName ?? string.Empty,
                          LastName = x.First().LastName ?? string.Empty,
                          MiddleName = x.First().MiddleName ?? string.Empty,
                          Nationality = x.First().Nationality ?? string.Empty,
                          Subscriptions = x.Select(x => new UserSubscriptions
                          {
                              Service = x.Service,
                              SubscriptionStatus = x.SubscriptionStatus == CoreObject.Enum.StatusEnum.Active ? ErrorMessage.UserSubscribed
                                                      : x.SubscriptionStatus == CoreObject.Enum.StatusEnum.Inactive ? ErrorMessage.UserUnSubscribed : ErrorMessage.UserErrorSubscription,
                              SubscriptionDate = x.SubscriptionDate
                          }).ToList(),
                      }).ToList();
        }

        public async Task<UserLoginResponse> Login(UserLoginInput request)
        {
            var response = new UserLoginResponse()
            {
                RequestStatus = false,
                Message = "Failed"
            };


            var passwordBtye = await _passwordManagement.EncryptAsync(request.Password, _appSettings.EncryptionPassPhrase);
            var encryptedPassword = Encoding.Default.GetString(passwordBtye);

            #region Get User

            var user = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Password == encryptedPassword);

            if (user == null)
            {

               response.Message = "Invalid user details";
               return response;
            }
            if (user.IsActive != CoreObject.Enum.StatusEnum.Active)
            {

                response.Message = "User Disabled";
                return response;
            }
            #endregion


            #region Get Service

            var service = await _appDbContext.PisiServices.FirstOrDefaultAsync(s => s.Id == request.Service_id);

            if (service == null)
            {

                response.Message = "Invalid user details";
                return response;
            }
            #endregion



            #region userToken

            var exisingToken  = await _appDbContext.AccessTokens.FirstOrDefaultAsync(token => token.UserId == user.Id);

            #endregion

            DateTime date = DateTime.Now;
            var token = AuthTokenManagement.WriteJwt(user, _appSettings);

            if (exisingToken == null)
            {
                var userToken = new AccessToken
                {
                    Token = token,
                    CreatedDate = date,
                    UpdatedDate = date,
                    UserId = user.Id,
                    ValidMinutes = _appSettings.TokenDefaultValidMinutes
                };

                await _appDbContext.AccessTokens.AddAsync(userToken);
            }
            else
            {
                exisingToken.UpdatedDate = date;
                exisingToken.Token = token;
            }

            int result = await _appDbContext.SaveChangesAsync();
            if (result > 0)
            {
                response.Token = token;
                response.Message = "Successful";
                response.RequestStatus = true;
            }
            else
            {
                response.Message = "Internal Error, try again";
            }

            return response;
        }

        public async Task<GlobalResponse> Register(UserRegisterationInput request)
        {
            var response = new GlobalResponse()
            {
                RequestStatus = false,
                Message = "Failed"
            };


            var passwordBtye = await _passwordManagement.EncryptAsync(request.Password, _appSettings.EncryptionPassPhrase);
            var encryptedPassword = Encoding.Default.GetString(passwordBtye);

            #region Get User

            var user = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Password == encryptedPassword ||
                                                                     user.PhoneNumber == request.PhoneNumber);
            #endregion


            if (user != null)
            {
                if (user.PhoneNumber == request.PhoneNumber)
                {
                    response.Message = "Phone Number already exist";
                }
                else
                {
                    response.Message = "Similar password already exist, please use another";
                }

                return response;
            }

            byte[] salt = Encoding.ASCII.GetBytes(_appSettings.Salt);
            DateTime date = DateTime.Now;
            var newUser = new User
            {
                PhoneNumber = request.PhoneNumber,
                Password = encryptedPassword,
                HashPassword = _passwordManagement.HashPasword(request.Password, salt),
                CreatedDate = date,
                UpdatedDate = date,
                Address = request.Address,
                Age = request.Age,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Nationality = request.Nationality,
                IsActive = CoreObject.Enum.StatusEnum.Active
            };

            await _appDbContext.Users.AddAsync(newUser);
            int result = await _appDbContext.SaveChangesAsync();
            if (result > 0)
            {
                response.Message = "Successful";
                response.RequestStatus = true;
            }
            else
            {
                response.Message = "Internal Error, try again";
            }

            return response;

        }
    }
}
