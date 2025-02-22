using AutoMapper;
using MediatR;
using Store.Core.Queries;
using Store.Domain.DTOs;
using Store.Domain.DTOs.Category;
using Store.Domain.Enums;
using Store.Infra.Sql.Repositories.CategoryRepo;
using Store.Shared.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.UseCases.Category.Queries
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, ResultDto<List<GetAllCategoryDto>>>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public GetAllCategoryQueryHandler(ICategoryRepository categoryRepository,IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<ResultDto<List<GetAllCategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await categoryRepository.GetAllCategoriesAsync();

            return ResultDto<List<GetAllCategoryDto>>.ReturnData(result, (int)EnumResponseStatus.OK, (int)EnumResultCode.Success, EnumResultCode.Success.GetDisplayName());
        }
    }
}
