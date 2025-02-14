using MediatR;
using Store.Domain;
using Store.Domain.DTOs;
using Store.Domain.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Commands
{
    public record AddCategoryCommand(AddCategoryCommandDto AddDto) : IRequest<ResultDto<Unit>>
    {
        
    }
}
