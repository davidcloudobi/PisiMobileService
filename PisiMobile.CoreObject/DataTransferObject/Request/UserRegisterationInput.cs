using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PisiMobile.CoreObject.DataTransferObject.Request
{
    public class UserRegisterationInput
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]  
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
    }
}
