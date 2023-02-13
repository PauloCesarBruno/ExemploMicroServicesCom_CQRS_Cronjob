using AutoMapper;
using Domain.Models;
using Infrastructure.Publishers.Messages;

namespace Application.Profiles
{
    public class PartNumberProfile : Profile
    {
        public PartNumberProfile()
        {
            CreateMap<PartNumber, PartNumberMessage>();
        }
    }
}
