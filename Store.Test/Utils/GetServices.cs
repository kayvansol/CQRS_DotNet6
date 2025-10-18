using Store.Api.Rest.Mapper;

namespace Store.Test.Utils
{
    public class GetServices
    {
        public static IMapper GetMapper()
        {

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new MapperProfile());
            });

            return mapperConfig.CreateMapper();

        }

        public static StoreContext GetStoreContext()
        {
            return new StoreContext();
        }

    }
}
