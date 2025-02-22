using AutoMapper;
using MediatR;
using Store.Core.Commands;
using Store.Domain.DTOs;
using Store.Domain.Enums;
using Store.Infra.Sql.Repositories.CategoryRepo;
using Store.Shared.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.UseCases.Category.Commands
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, ResultDto<Unit>>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public AddCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<ResultDto<Unit>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = mapper.Map<Domain.Category>(request.AddDto);

            await categoryRepository.CreateAsync(category);

            return ResultDto<Unit>.ReturnData(Unit.Value, (int)EnumResponseStatus.OK, (int)EnumResultCode.Success, EnumResultCode.Success.GetDisplayName());
        }
    }
}
