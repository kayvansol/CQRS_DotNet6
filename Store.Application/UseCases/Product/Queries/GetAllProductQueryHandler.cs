using AutoMapper;
using MediatR;
using Store.Core.Queries;
using Store.Domain.DTOs;
using Store.Domain.DTOs.Product;
using Store.Domain.Enums;
using Store.Infra.Sql.Repositories.ProductRepo;
using Store.Shared.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.UseCases.Product.Queries
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, ResultDto<List<GetAllProductDto>>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public GetAllProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ResultDto<List<GetAllProductDto>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var result = await productRepository.GetAllProductsAsync();

            return ResultDto<List<GetAllProductDto>>.ReturnData(result, (int)EnumResponseStatus.OK, (int)EnumResultCode.Success, EnumResultCode.Success.GetDisplayName());
        }
    }
}
