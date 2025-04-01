
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
