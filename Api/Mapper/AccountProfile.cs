using AutoMapper;
using Api.Domain;
using Schema;

namespace Api.Mapper;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountRequest, Account>().ReverseMap();
        CreateMap<AccountResponse, Account>().ReverseMap();
    }
}
