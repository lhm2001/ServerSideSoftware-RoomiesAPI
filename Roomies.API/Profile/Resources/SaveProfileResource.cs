using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Resources
{
    public class SaveProfileResource
    {
		[Required]
		[MaxLength(50)]
		public string Name { get; set; }
		[Required]
		[MaxLength(50)]
		public string LastName { get; set; }
		[Required]
		[MaxLength(9)]
		public string CellPhone { get; set; }
		[Required]
		[MaxLength(8)]
		public string IdCard { get; set; }
		[Required]
		[MaxLength(240)]
		public string Description { get; set; }
		[Required]
		public string Birthday { get; set; }
		[Required]
		[MaxLength(25)]
		public string Department { get; set; }
		[Required]
		[MaxLength(25)]
		public string Province { get; set; }
		[Required]
		[MaxLength(25)]
		public string District { get; set; }
		[Required]
		[MaxLength(100)]
		public string Address { get; set; }

		public string ProfilePicture { get; set; }
		public bool Verified { get; set; }
	}
}
