using AutoMapper;
using Api.Domain;
using Schema;

namespace Api.Mapper;

public class CustomerAddressProfile : Profile
{
    public CustomerAddressProfile()
    {
        CreateMap<CustomerAddressRequest, CustomerAddress>().ReverseMap();
        CreateMap<CustomerAddressResponse, CustomerAddress>().ReverseMap();
    }
}
