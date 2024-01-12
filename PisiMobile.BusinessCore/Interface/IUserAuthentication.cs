using PisiMobile.CoreObject.DataTransferObject.Request;
using PisiMobile.CoreObject.DataTransferObject.Response;
using PisiMobile.CoreObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PisiMobile.BusinessCore.Interface
{
    public interface IUserAuthentication
    {
        Task<GlobalResponse> Register(UserRegisterationInput request);  
        Task<UserLoginResponse> Login(UserLoginInput request);
        Task<IEnumerable<UserSubscriptionResponse>> GetActiveUserSubscriptions();

    }
}
