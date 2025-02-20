using Store.Domain.DTOs.Category;

namespace Store.Test.Data.Category
{
    public class CategoryTestData
    {

        public static IEnumerable<object[]> addCategoryCommandDto()
        {
            List<AddCategoryCommandDto[]> data = new List<AddCategoryCommandDto[]>();

            AddCategoryCommandDto[] adds = new AddCategoryCommandDto[1];

            adds[0] = new AddCategoryCommandDto()
            {
                CategoryName = "Dell G9 Server"
            };

            data.Add(adds);

            return data;
                        
        }
        
    }
}
