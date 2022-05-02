using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Resources
{
    public class PaymentMethodResource
    {
        public int Id { get; set; }
        public string CVV { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
