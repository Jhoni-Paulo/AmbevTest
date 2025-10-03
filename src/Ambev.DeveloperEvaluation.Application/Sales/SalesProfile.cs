using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales;

public class SalesProfile : Profile
{
    public SalesProfile()
    {
        CreateMap<Sale, CreateSaleResult>()
            .ForMember(dest => dest.SaleId, opt => opt.MapFrom(src => src.Id));

        CreateMap<Sale, GetSaleByIdResult>();
        CreateMap<SaleItem, GetSaleByIdResult.SaleItemResult>();
    }
}