using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PisiMobile.CoreObject.Models
{
    public class AccessToken : BaseClass
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public int ValidMinutes { get; set; }
        public bool IsValid => UpdatedDate.AddMinutes(ValidMinutes) > DateTime.Now;
    }
}
