using AutoMapper;
using Api.Domain;
using Schema;

namespace Api.Mapper;

public class MoneyTransferProfile : Profile
{
    public MoneyTransferProfile()
    {
        CreateMap<MoneyTransferRequest, MoneyTransfer>().ReverseMap();
        CreateMap<MoneyTransferResponse, MoneyTransfer>().ReverseMap();
    }
}
