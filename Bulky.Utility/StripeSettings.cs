using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public class StripeSettings
    {

        // names must be exact like in appsettings.json
        public string PublishableKey { get; set; }
        public string SecretKey { get; set; }
    }
}
