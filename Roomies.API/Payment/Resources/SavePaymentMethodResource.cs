using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Resources
{
    public class SavePaymentMethodResource
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(3)]
        public string CVV { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
    }
}
