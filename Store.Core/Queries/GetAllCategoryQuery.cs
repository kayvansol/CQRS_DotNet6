using MediatR;
using Store.Domain.DTOs;
using Store.Domain.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Queries
{
    public record GetAllCategoryQuery(int statrtPage,int pageSize) : IRequest<ResultDto<List<GetAllCategoryDto>>>
    {

    }
}
