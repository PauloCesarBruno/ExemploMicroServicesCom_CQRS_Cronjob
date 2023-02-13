using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;

namespace Infrastructure.Data.Profiles
{
    public class PartNumberEntityProfile : Profile
    {
        public PartNumberEntityProfile()
        {
            CreateMap<PartNumberEntity, PartNumber>()
                .ForMember(d => d.PnInterchangeable, o => o.MapFrom(s => s.PN_INTERCHANGEABLE))
                .ForMember(d => d.Pn, o => o.MapFrom(s => s.PN))
                .ForMember(d => d.PnDescription, o => o.MapFrom(s => s.PN_DESCRIPTION))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.CATEGORY))
                .ForMember(d => d.StockUom, o => o.MapFrom(s => s.STOCK_UOM))
                .ForMember(d => d.HazardousMaterial, o => o.MapFrom(s => s.HAZARDOUS_MATERIAL))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.STATUS))
                .ForMember(d => d.ShelfLifeFlag, o => o.MapFrom(s => s.SHELF_LIFE_FLAG))
                .ForMember(d => d.ShelfLifeDays, o => o.MapFrom(s => s.SHELF_LIFE_DAYS))
                .ForMember(d => d.Chapter, o => o.MapFrom(s => s.CHAPTER))
                .ForMember(d => d.Section, o => o.MapFrom(s => s.SECTION))
                .ForMember(d => d.AvarangeCost, o => o.MapFrom(s => s.AVERAGE_COST))
                .ForMember(d => d.StandardCost, o => o.MapFrom(s => s.STANDARD_COST))
                .ForMember(d => d.SecondaryCost, o => o.MapFrom(s => s.SECONDARY_COST))
                .ForMember(d => d.ToolCalibrationFlag, o => o.MapFrom(s => s.TOOL_CALIBRATION_FLAG))
                .ForMember(d => d.ToolLifeDays, o => o.MapFrom(s => s.TOOL_LIFE_DAYS))
                .ForMember(d => d.HazardousMateriaNo, o => o.MapFrom(s => s.HAZARDOUS_MATERIAL_NO))
                .ForMember(d => d.ToolControlItem, o => o.MapFrom(s => s.TOOL_CONTROL_ITEM))
                .ForMember(d => d.InventoryType, o => o.MapFrom(s => s.INVENTORY_TYPE))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(s => s.CREATED_DATE))
                .ForMember(d => d.ModifiedDate, o => o.MapFrom(s => s.MODIFIED_DATE))
                .ForMember(d => d.ValidationResults, o => o.Ignore());
        }
    }
}
