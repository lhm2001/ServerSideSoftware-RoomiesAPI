using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Models
{
    public class FavouritePost
    {
        public int PostId { set; get; }
        public Post Post { set; get; }
        public int LeaseholderId { set; get; }
        public Leaseholder Leaseholder { set; get; }

    }
}
