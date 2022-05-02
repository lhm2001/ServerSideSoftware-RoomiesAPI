using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Resources
{
    public class PlanResource
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public DateTime StartSubscription { set; get; }
        //public DateTime EndSubsciption { set; get; }
    }
}
