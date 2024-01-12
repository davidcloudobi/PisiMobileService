using PisiMobile.CoreObject.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PisiMobile.CoreObject.DataTransferObject.Response    
{   
    public class UserSubscriptionResponse
    {   
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public List<UserSubscriptions> Subscriptions { get; set; }  

    }
    public class UserSubscriptions
    {
        public string Service { get; set; }
        public string SubscriptionStatus { get; set; }
        public DateTime SubscriptionDate { get; set; }
    }
}
