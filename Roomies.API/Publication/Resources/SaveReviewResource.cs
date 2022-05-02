using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Resources
{
    public class SaveReviewResource
    {
        [Required]
        [MaxLength(300)]
        public string Content { set; get; }

    }
}
