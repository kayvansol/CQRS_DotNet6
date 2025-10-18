
namespace Store.Core.Validators
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {

            RuleFor(x => x.AddDto.ProductName).NotEmpty().WithMessage("مقدار نام محصول را وارد نمایید");

            RuleFor(x => x.AddDto.Price).GreaterThan(0).WithMessage("مقدار قیمت باید بزرگتر از صفر باشد");

        }
    }
}
