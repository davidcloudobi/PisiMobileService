using PisiMobile.BusinessCore.Interface;
using PisiMobile.CoreObject.DataTransferObject.Request;
using PisiMobile.CoreObject.DataTransferObject.Response;
using PisiMobile.CoreObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PisiMobile.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthenticationController : ControllerBase
    {
        private IUserAuthentication _userAuthentication;

        public UserAuthenticationController(IUserAuthentication userAuthentication)
        {
            _userAuthentication = userAuthentication;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponse>> Login([FromBody]UserLoginInput request)
        {
            var result = await _userAuthentication.Login(request);
            return result.RequestStatus ? Ok(result) : BadRequest(result);
        }
    }
}
