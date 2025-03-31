using AutoMapper;
using Api.Domain;
using Schema;

namespace Api.Mapper;

public class EftTransactionProfile : Profile
{
    public EftTransactionProfile()
    {
        CreateMap<EftTransactionRequest, EftTransaction>().ReverseMap();
        CreateMap<EftTransactionResponse, EftTransaction>().ReverseMap();
    }
}
