using AutoMapper;
using Api.Domain;
using Schema;

namespace Api.Mapper;

public class CustomerPhoneProfile : Profile
{
    public CustomerPhoneProfile()
    {
        CreateMap<CustomerPhoneRequest, CustomerPhone>().ReverseMap();
        CreateMap<CustomerPhoneResponse, CustomerPhone>().ReverseMap();
    }
}
