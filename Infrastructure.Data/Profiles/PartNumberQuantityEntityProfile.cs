using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Profiles
{
    public class PartNumberQuantityEntityProfile : Profile
    {
        public PartNumberQuantityEntityProfile()
        {
            CreateMap<PartNumberQuantityEntity, PartNumberQuantity>()
                .ForMember(d => d.Location, o => o.MapFrom(s => s.LOCATION))
                .ForMember(d => d.Pn, o => o.MapFrom(s => s.PN))
                .ForMember(d => d.Sn, o => o.MapFrom(s => s.PN))
                .ForMember(d => d.PnInterchangeable, o => o.MapFrom(s => s.PN_INTERCHANGEABLE))
                .ForMember(d => d.PnDescription, o => o.MapFrom(s => s.PN_DESCRIPTION))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.CATEGORY))
                .ForMember(d => d.Vendor, o => o.MapFrom(s => s.VENDOR))
                .ForMember(d => d.QtyAvailable, o => o.MapFrom(s => s.QTY_AVAILABLE))
                .ForMember(d => d.QtyReserved, o => o.MapFrom(s => s.QTY_RESERVED))
                .ForMember(d => d.QtyInTransfer, o => o.MapFrom(s => s.QTY_IN_TRANSFER))
                .ForMember(d => d.QtyPendingRi, o => o.MapFrom(s => s.QTY_PENDING_RI))
                .ForMember(d => d.QtyUs, o => o.MapFrom(s => s.QTY_US))
                .ForMember(d => d.QtyInRepair, o => o.MapFrom(s => s.QTY_IN_REPAIR))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(s => s.CREATED_DATE))
                .ForMember(d => d.ModifiedDate, o => o.MapFrom(s => s.MODIFIED_DATE))
                .ForMember(d => d.ValidationResults, o => o.Ignore());
        }
    }
}
