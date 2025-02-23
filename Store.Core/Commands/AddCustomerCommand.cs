using MediatR;
using Store.Domain.DTOs;
using Store.Domain.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Commands
{
    public record AddCustomerCommand(AddCustomerCommandDto AddDto) : IRequest<ResultDto<Unit>>
    {

    }
}
