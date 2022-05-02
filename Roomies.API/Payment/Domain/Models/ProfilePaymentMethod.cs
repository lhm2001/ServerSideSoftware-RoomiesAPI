using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Models
{
    public class ProfilePaymentMethod
    {
        public int ProfileId { get; set; }
        public int PaymentMethodId { get; set; }
        public Profile Profile { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
