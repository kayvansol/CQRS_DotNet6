using AutoMapper;
using MediatR;
using Store.Core.Queries;
using Store.Domain.DTOs;
using Store.Domain.DTOs.Customer;
using Store.Domain.Enums;
using Store.Infra.Sql.Repositories.CustomerRepo;
using Store.Shared.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.UseCases.Customer.Queries
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, ResultDto<List<GetAllCustomerDto>>>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public GetAllCustomerQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        public async Task<ResultDto<List<GetAllCustomerDto>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            var result = await customerRepository.GetAllCustomersAsync();

            return ResultDto<List<GetAllCustomerDto>>.ReturnData(result, (int)EnumResponseStatus.OK, (int)EnumResultCode.Success, EnumResultCode.Success.GetDisplayName());
        }
    }
}
