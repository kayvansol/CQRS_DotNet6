
namespace Store.Core.Validators
{
    public class AddCategoryCommandValidator: AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidator()
        {
            RuleFor(x => x.AddDto.CategoryName).NotEmpty().WithMessage("مقدار نام را وارد نمایید");

            RuleFor(x => x.AddDto.CategoryName).Length(2, 4).WithMessage("طول رشته خلاصه بین 2 و 4 می باشد");

        }
    }
}
