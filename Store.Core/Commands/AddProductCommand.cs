﻿using Store.Domain.DTOs.Product;

namespace Store.Core.Commands
{
    public record AddProductCommand(AddProductCommandDto AddDto) : IRequest<ResultDto<Unit>>
    {

    }
}
