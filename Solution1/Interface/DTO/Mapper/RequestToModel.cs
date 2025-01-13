using AutoMapper;
using Interface.DTO.Request;
using Support.Models;

namespace Interface.DTO.Mapper;

public class RequestToModel : Profile
{
    public RequestToModel()
    {
        CreateMap<UserRequestDTO, User>();
        CreateMap<InvoiceRequestDTO, Invoice>();
        CreateMap<UsageRequestDTO, Usage>();
    }
}