using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PisiMobile.CoreObject
{
    public class AppSettings
    {   
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Salt { get; set; }
        public string EncryptionPassPhrase { get; set; }
        public int TokenDefaultValidMinutes { get; set; }
    }
}
