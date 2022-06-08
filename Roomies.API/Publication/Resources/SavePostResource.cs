using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Resources
{
    public class SavePostResource
    {
        [Required]
        [MaxLength(100)]
        public string Title { set; get; }
        [Required]
        [MaxLength(500)]
        public string Description { set; get; }

        [Required]
        [MaxLength(50)]
        public string Address { set; get; }
        public string Photo { set; get; }
        public string Province { set; get; }
        [Required]
        [MaxLength(25)]
        public string District { set; get; }
        [Required]
        [MaxLength(25)]
        public string Department { set; get; }
        [Required]
        public float Price { set; get; }
        [Required]
        public int RoomQuantity { set; get; }
        [Required]
        public int BathroomQuantity { set; get; }
    }
}
