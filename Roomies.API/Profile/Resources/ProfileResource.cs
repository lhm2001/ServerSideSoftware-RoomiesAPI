using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Resources
{
    public class ProfileResource
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string CellPhone { get; set; }
		public string IdCard { get; set; }
		public string Description { get; set; }
		public string Birthday { get; set; }
		public string Department { get; set; }
		public string Province { get; set; }
		public string District { get; set; }
		public string ProfilePicture { get; set; }
		public bool Verified { get; set; }
		public PlanResource Plan{ get; set; }
		public DateTime StartSubscription { get; set; }
		public DateTime EndSubsciption { get; set; }
	}
}
