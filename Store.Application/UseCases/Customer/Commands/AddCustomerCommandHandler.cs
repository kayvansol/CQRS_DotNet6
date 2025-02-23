using AutoMapper;
using MediatR;
using Store.Core.Commands;
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

namespace Store.Application.UseCases.Customer.Commands
{
    public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, ResultDto<Unit>>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public AddCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        public async Task<ResultDto<Unit>> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            var category = mapper.Map<Domain.Customer>(request.AddDto);

            await customerRepository.CreateAsync(category);

            return ResultDto<Unit>.ReturnData(Unit.Value, (int)EnumResponseStatus.OK, (int)EnumResultCode.Success, EnumResultCode.Success.GetDisplayName());
        }
    }
}
