using PisiMobile.BusinessCore.Interface;
using PisiMobile.BusinessCore.Service;
using PisiMobile.CoreObject.DataTransferObject.Request;
using PisiMobile.CoreObject.DataTransferObject.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PisiMobile.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private ISubscriptions _subscriptions;

        public SubscriptionController(ISubscriptions subscriptions)
        {
            _subscriptions = subscriptions;
        }

        [HttpPost("subscribe")]
        public async Task<ActionResult<UserLoginResponse>> Subscribe([FromBody] SubscribeInput request)
        {
            var result = await _subscriptions.Subscribe(request);
            return result.RequestStatus ? Ok(result) : BadRequest(result);
        }

        [HttpPost("unsubscribe")]
        public async Task<ActionResult<GlobalResponse>> UnSubscribe([FromBody] UnSubscribeInput request)
        {
            var result = await _subscriptions.UnSubscribe(request);
            return result.RequestStatus ? Ok(result) : BadRequest(result);
        }

        [HttpPost("checkStatus")]
        public async Task<ActionResult<SubscriptionCheckStatusResponse>> CheckStatus([FromBody] SubscriptionCheckStatusInput request)
        {
            var result = await _subscriptions.CheckStatus(request);
            return result.RequestStatus ? Ok(result) : BadRequest(result);
        }
    }
}
