using AutoMapper;
using Api.Domain;
using Schema;

namespace Api.Mapper;

public class AccountTransactionProfile : Profile
{
    public AccountTransactionProfile()
    {
        CreateMap<AccountTransactionRequest, AccountTransaction>().ReverseMap();
        CreateMap<AccountTransactionResponse, AccountTransaction>().ReverseMap();
    }
}
