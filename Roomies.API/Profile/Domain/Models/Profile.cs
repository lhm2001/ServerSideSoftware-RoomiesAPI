using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Models
{
    public class Profile
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
		public string Address { get; set; }
		public string ProfilePicture { get; set; }
		public bool Verified { get; set; }
		public List<ProfilePaymentMethod> ProfilePaymentMethods { get; set; }

		public int PlanId { set; get; } 
		public Plan Plan { set; get; }

		public int UserId { get; set; }
		public User User { get; set; }

		public DateTime StartSubscription { set; get; }
		public DateTime EndSubsciption { set; get; }

        
    }
}
