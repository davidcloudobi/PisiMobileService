using PisiMobile.CoreObject.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PisiMobile.CoreObject.Models
{
    public class PisiService : BaseClass    
    {
        public string Name { get; set; }    
        public decimal Amount { get; set; }
        public StatusEnum Status { get; set; }
    }
}
