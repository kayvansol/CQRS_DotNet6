using FluentValidation;
using Store.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Validators
{
    public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
    {
        public AddCustomerCommandValidator()
        {
            RuleFor(c => c.AddDto.Email).EmailAddress().WithMessage("ایمیل را به درستی وارد نمایید");
        }
    }
}
