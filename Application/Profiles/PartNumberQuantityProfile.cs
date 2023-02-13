using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Publishers.Messages;

namespace Application.Profiles
{
    public class PartNumberQuantityProfile : Profile
    {
        public PartNumberQuantityProfile()
        {
            CreateMap<PartNumberQuantity, PartNumberQuantityMessages>().ReverseMap();
           // CreateMap<Inventory, InventoryEntity>().ReverseMap();
        }
    }
}
