using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PisiMobile.BusinessCore.Interface;
using PisiMobile.BusinessCore.Service;
using PisiMobile.CoreObject.DataTransferObject.Request;
using PisiMobile.CoreObject.DataTransferObject.Response;
using PisiMobile.CoreObject.Models;

namespace PisiMobile.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        private IUserAuthentication _userAuthentication;
        private IServices _services;

        public UtilityController(IUserAuthentication userAuthentication, IServices services)
        {
            _userAuthentication = userAuthentication;
            _services = services;
        }


        [HttpPost("add-new-user")]
        public async Task<ActionResult<GlobalResponse>> Register([FromBody] UserRegisterationInput request)
        {
            var result = await _userAuthentication.Register(request);
            return result.RequestStatus ? Ok(result) : BadRequest(result);
        }

        [HttpGet("get-active-user-subscriptions")]
        public async Task<ActionResult<IEnumerable<UserSubscriptionResponse>>> GetActiveUserSubscriptions()
        {
            var ListOfCustomers = await _userAuthentication.GetActiveUserSubscriptions();
            return ListOfCustomers.Any() ? Ok(ListOfCustomers) : NoContent();
        }

        [HttpPost("add-new-service")]
        public async Task<ActionResult<GlobalResponse>> Create([FromBody] CreateServiceInput request)
        {
            var result = await _services.Create(request);
            return result.RequestStatus ? Ok(result) : BadRequest(result);
        }


        [HttpGet("get-service-by-id")]
        public async Task<ActionResult<GlobalResponse<PisiService>>> GetById([FromQuery] Guid serviceId)
        {
            var result = await _services.GetById(serviceId);
            return result.RequestStatus ? Ok(result) : BadRequest(result);
        }

        [HttpGet("get-service-by-name")]
        public async Task<ActionResult<GlobalResponse<PisiService>>> GetByName([FromQuery] string serviceName)
        {
            var result = await _services.GetByName(serviceName);
            return result.RequestStatus ? Ok(result) : BadRequest(result);
        }

        [HttpGet("get-all-service")]
        public async Task<ActionResult<GlobalResponse<PisiService>>> GetAll()
        {
            var result = await _services.GetAll();
            return result.RequestStatus ? Ok(result) : BadRequest(result);
        }

        [HttpGet("get-all-inactive-service")]
        public async Task<ActionResult<GlobalResponse<PisiService>>> GetAllInactive()
        {
            var result = await _services.GetAllInactive();
            return result.RequestStatus ? Ok(result) : BadRequest(result);
        }


        [HttpGet("get-all-active-service")]
        public async Task<ActionResult<GlobalResponse<PisiService>>> GetAllActive()
        {
            var result = await _services.GetAllActive();
            return result.RequestStatus ? Ok(result) : BadRequest(result);
        }

        [HttpPut("deactivate-service-by-id")]
        public async Task<ActionResult<GlobalResponse<PisiService>>> DeactivateById([FromQuery] Guid serviceId)
        {
            var result = await _services.DeactivateById(serviceId);
            return result.RequestStatus ? Ok(result) : BadRequest(result);
        }

        [HttpPut("deactivate-service-by-name")]
        public async Task<ActionResult<GlobalResponse<PisiService>>> DeactivateByName([FromQuery] string serviceName)
        {
            var result = await _services.DeactivateByName(serviceName);
            return result.RequestStatus ? Ok(result) : BadRequest(result);
        }

        [HttpPut("activate-service-by-id")]
        public async Task<ActionResult<GlobalResponse<PisiService>>> ActivateById([FromQuery] Guid serviceId)
        {
            var result = await _services.ActivateById(serviceId);
            return result.RequestStatus ? Ok(result) : BadRequest(result);
        }

        [HttpPut("activate-by-name")]
        public async Task<ActionResult<GlobalResponse<PisiService>>> ActivateByName([FromQuery] string serviceName)
        {
            var result = await _services.ActivateByName(serviceName);
            return result.RequestStatus ? Ok(result) : BadRequest(result);
        }
    }
}
