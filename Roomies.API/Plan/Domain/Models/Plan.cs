using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Models
{
    public class Plan
    {
        public int Id { set; get; }
        public float Price { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        
        public List<Profile> Profiles { set; get; }
    }
}
