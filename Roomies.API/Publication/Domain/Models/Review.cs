using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Models
{
    public class Review
    {
        public int Id { set; get; }
        public string Content { set; get; }
        public DateTime Date { set; get; }
        public Leaseholder Leaseholder { set; get; }
        public int LeaseholderId { set; get; }
        public Post Post { set; get; }
        public int PostId { set; get; }

    }
}
