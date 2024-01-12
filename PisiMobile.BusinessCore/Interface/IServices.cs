using PisiMobile.CoreObject.DataTransferObject.Request;
using PisiMobile.CoreObject.DataTransferObject.Response;
using PisiMobile.CoreObject.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PisiMobile.BusinessCore.Interface
{
    public interface IServices
    {   
        Task<GlobalResponse> Create(CreateServiceInput request);
        Task<GlobalResponse<CoreObject.Models.PisiService>> GetById(Guid serviceId);
        Task<GlobalResponse<CoreObject.Models.PisiService>> GetByName(string serviceName);
        Task<GlobalResponse<List<CoreObject.Models.PisiService>>> GetAll();
        Task<GlobalResponse<List<CoreObject.Models.PisiService>>> GetAllInactive();
        Task<GlobalResponse<List<CoreObject.Models.PisiService>>> GetAllActive();
        Task<GlobalResponse> DeactivateById(Guid serviceId);
        Task<GlobalResponse> DeactivateByName(string serviceName);
        Task<GlobalResponse> ActivateById(Guid serviceId);
        Task<GlobalResponse> ActivateByName(string serviceName);
    }
}
