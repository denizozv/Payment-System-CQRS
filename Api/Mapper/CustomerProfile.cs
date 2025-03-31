using AutoMapper;
using Schema;

namespace Api.Mapper;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CustomerRequest, Customer>().ReverseMap();
        CreateMap<CustomerResponse, Customer>().ReverseMap();
    }
}