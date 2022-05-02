using Roomies.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Publication.Domain.Models
{
    public class Rule
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public int PostId { set; get; }
        public Post Post{ set; get; }
    }
}
