using Roomies.API.Publication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Models
{
    public class Post
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Description {set;get;}
        public string Address { set; get; }
        public string Province { set; get; }
        public string District { set; get; }
        public string Department { set; get; }
        public float Price { set; get; }
        public int RoomQuantity { set; get; }
        public int BathroomQuantity { set; get; }
        public string Photo { set; get; }
        public DateTime PostDate { set; get; }
        public List<Review> Reviews { set; get; }
        public List<FavouritePost> FavouritePosts { set; get; }
        public Landlord Landlord { set; get; }
        public int LandlordId { set; get; }
        public List<Rule> Rules { set; get; }

    }
}
