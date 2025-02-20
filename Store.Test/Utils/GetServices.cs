using AutoMapper;
using Store.Api.Rest.Mapper;
using Store.Domain.Entities;
using Store.Infra.Sql.Context;
using Store.Infra.Sql.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
