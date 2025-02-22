using AutoMapper;
using MediatR;
using Store.Core.Commands;
using Store.Domain.DTOs;
using Store.Domain.Enums;
using Store.Infra.Sql.Repositories.ProductRepo;
using Store.Shared.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.UseCases.Product.Commands
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ResultDto<Unit>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public AddProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ResultDto<Unit>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var category = mapper.Map<Domain.Product>(request.AddDto);

            await productRepository.CreateAsync(category);

            return ResultDto<Unit>.ReturnData(Unit.Value, (int)EnumResponseStatus.OK, (int)EnumResultCode.Success, EnumResultCode.Success.GetDisplayName());
        }
    }
}
