using AutoMapper;
using Tech.Domain.DTO;
using Tech.Domain.Entity;

namespace Tech.Application.Mapping;

public class ReportMapping : Profile
{
    public ReportMapping()
    {
        CreateMap<Article, ReportDto>()
            .ForCtorParam(ctorParamName: "Id", m => m.MapFrom(s => s.Id))
            .ForCtorParam(ctorParamName: "Name", m => m.MapFrom(s => s.Name))
            .ReverseMap();
    }
}

