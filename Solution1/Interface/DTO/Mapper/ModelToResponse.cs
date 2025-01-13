using AutoMapper;
using Interface.DTO.Response;
using Support.Models;

namespace Interface.DTO.Mapper;

public class ModelToResponse : Profile
{
    public ModelToResponse()
    {
        CreateMap<User, UserResponseDTO>();
        CreateMap<Invoice, InvoiceResponseDTO>();
    }
}