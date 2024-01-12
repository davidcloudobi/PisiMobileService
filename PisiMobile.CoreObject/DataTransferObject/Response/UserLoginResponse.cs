using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PisiMobile.CoreObject.DataTransferObject.Response
{
    public class UserLoginResponse : GlobalResponse
    {
        public string Token { get; set; }
    }
}
