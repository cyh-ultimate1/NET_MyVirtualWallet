using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVirtualWallet.WebAPI.Configuration
{
    public class JwtConfig
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
