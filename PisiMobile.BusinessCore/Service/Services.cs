using PisiMobile.BusinessCore.Interface;
using PisiMobile.CoreObject.DataTransferObject.Request;
using PisiMobile.CoreObject.DataTransferObject.Response;
using PisiMobile.CoreObject.Enum;
using PisiMobile.CoreObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PisiMobile.BusinessCore.Service
{
    public class Services : IServices
    {
        private DatabaseContext _appDbContext;

        public Services(DatabaseContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<GlobalResponse> ActivateById(Guid serviceId)
        {
            var response = new GlobalResponse()
            {
                RequestStatus = false,
                Message = "Failed"
            };

            var service  = await _appDbContext.PisiServices.FirstOrDefaultAsync(s => s.Id == serviceId);

            if (service != null)
            {
                if (service.Status == StatusEnum.Active)
                {
                    response.Message = "Service already active";
                }
                else
                {
                    service.Status = StatusEnum.Active;
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

                }
                               

            }
            return response;
        }

        public async Task<GlobalResponse> ActivateByName(string serviceName)
        {

            var response = new GlobalResponse()
            {
                RequestStatus = false,
                Message = "Failed"
            };

            var service = await _appDbContext.PisiServices.FirstOrDefaultAsync(s => s.Name == serviceName);

            if (service != null)
            {
                if (service.Status == StatusEnum.Active)
                {
                    response.Message = "Service already active";
                }
                else
                {
                    service.Status = StatusEnum.Active;
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

                }


            }
            return response;
        }

        public async Task<GlobalResponse> Create(CreateServiceInput request)
        {

            var response = new GlobalResponse()
            {
                RequestStatus = false,
                Message = "Failed"
            };

            var service = await _appDbContext.PisiServices.FirstOrDefaultAsync(s => s.Name == request.Name);

            if (service != null)
            {
                response.Message = "Service already exist";
            }
            else
            {
                var date = DateTime.Now;
                var newService = new CoreObject.Models.PisiService
                {
                    Amount = request.Amount,
                    Name = request.Name,
                    CreatedDate = date,
                    UpdatedDate = date,
                    Status = StatusEnum.Active
                };

                await _appDbContext.PisiServices.AddAsync(newService);
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
            }

            return response;
        }

        public async Task<GlobalResponse> DeactivateById(Guid serviceId)
        {
            var response = new GlobalResponse()
            {
                RequestStatus = false,
                Message = "Failed"
            };

            var service = await _appDbContext.PisiServices.FirstOrDefaultAsync(s => s.Id == serviceId);

            if (service != null)
            {
                if (service.Status == StatusEnum.Inactive)
                {
                    response.Message = "Service already Inactive";
                }
                else
                {
                    service.Status = StatusEnum.Inactive;
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

                }


            }
            return response;
        }

        public async Task<GlobalResponse> DeactivateByName(string serviceName)
        {
            var response = new GlobalResponse()
            {
                RequestStatus = false,
                Message = "Failed"
            };

            var service = await _appDbContext.PisiServices.FirstOrDefaultAsync(s => s.Name == serviceName);

            if (service != null)
            {
                if (service.Status == StatusEnum.Inactive)
                {
                    response.Message = "Service already Inactive";
                }
                else
                {
                    service.Status = StatusEnum.Inactive;
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

                }


            }
            return response;
        }

        public async Task<GlobalResponse<List<CoreObject.Models.PisiService>>> GetAll()
        {
            var services = await _appDbContext.PisiServices.ToListAsync();
            return new GlobalResponse<List<CoreObject.Models.PisiService>>
            {
                RequestStatus = true,
                Message = services.Any() ? "Successful" : "No Content",
                Data = services
            };
        }

        public async Task<GlobalResponse<List<CoreObject.Models.PisiService>>> GetAllActive()
        {
            var services = await _appDbContext.PisiServices.Where(service => service.Status == StatusEnum.Active).ToListAsync();
            return new GlobalResponse<List<CoreObject.Models.PisiService>>
            {
                RequestStatus = true,
                Message = services.Any() ? "Successful" : "No Content",
                Data = services
            };
        }

        public async Task<GlobalResponse<List<CoreObject.Models.PisiService>>> GetAllInactive()
        {
            var services = await _appDbContext.PisiServices.Where(service => service.Status == StatusEnum.Inactive).ToListAsync();
            return new GlobalResponse<List<CoreObject.Models.PisiService>>
            {
                RequestStatus = true,
                Message = services.Any() ? "Successful" : "No Content",
                Data = services
            };
        }

        public async Task<GlobalResponse<CoreObject.Models.PisiService>> GetById(Guid serviceId)
        {
            var service = await _appDbContext.PisiServices.FirstOrDefaultAsync(service => service.Id == serviceId);
            return new GlobalResponse<CoreObject.Models.PisiService>
            {
                RequestStatus = true,
                Message = service != null ? "Successful" : "No Content",
                Data = service
            };
        }

        public async Task<GlobalResponse<CoreObject.Models.PisiService>> GetByName(string serviceName)
        {
            var service = await _appDbContext.PisiServices.FirstOrDefaultAsync(service => service.Name == serviceName);
            return new GlobalResponse<CoreObject.Models.PisiService>
            {
                RequestStatus = true,
                Message = service != null ? "Successful" : "No Content",
                Data = service
            };
        }
    }
}
