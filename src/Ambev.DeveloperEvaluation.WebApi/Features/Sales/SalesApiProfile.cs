using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

public class SalesApiProfile : Profile
{
    public SalesApiProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<CreateSaleRequest.SaleItemRequest, CreateSaleCommand.SaleItemCommand>();
     
        CreateMap<CreateSaleResult, CreateSaleResponse>();

        CreateMap<GetSaleByIdResult, GetSaleByIdResponse>();
        CreateMap<GetSaleByIdResult.SaleItemResult, GetSaleByIdResponse.SaleItemResponse>();
    }
}