using MediatR;
using Store.Domain.DTOs;
using Store.Domain.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Commands
{
    public record AddOrderCommand(AddOrderCommandDto AddDto) : IRequest<ResultDto<Unit>>
    {

    }
}
