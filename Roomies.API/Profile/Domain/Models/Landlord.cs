using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Models
{
    public class Landlord : Profile
    {
        public List<Post> Posts { get; set; }
        
    }
}
