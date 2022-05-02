using AutoMapper;
using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using Roomies.API.Plan.Resources;
using Roomies.API.Publication.Domain.Models;
using Roomies.API.Publication.Resources;
using Roomies.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Mapping
{
    public class ResourceToModelProfile : AutoMapper.Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SavePaymentMethodResource, PaymentMethod>();
            CreateMap<SavePostResource, Post>();
            CreateMap<SaveReviewResource, Review>();
            CreateMap<SaveProfileResource, Domain.Models.Profile>();
            CreateMap<SaveLandlordResource, Landlord>();
            CreateMap<SaveLeaseholderResource, Leaseholder>();
            CreateMap<RegisterRequest, User>();
            CreateMap<SaveRuleResource, Rule>();
            CreateMap<SavePlanResource, Domain.Models.Plan>();
        }
    }
}
