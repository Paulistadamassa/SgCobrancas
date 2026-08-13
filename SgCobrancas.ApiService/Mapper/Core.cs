using AutoMapper;
using SgCobrancas.ApiService.DTOs;
using SgCobrancas.ApiService.Models;

namespace SgCobrancas.ApiService.Mapper;

public class Core : Profile
{
    public Core()
    {
        CreateMap<Customer, CustomerDTO>()
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Invoice, InvoiceDTO>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Name : null))
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore());
    }
}