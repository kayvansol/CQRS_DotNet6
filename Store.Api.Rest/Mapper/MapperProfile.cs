using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Store.Domain;
using Store.Domain.DTOs.Category;

namespace Store.Api.Rest.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {

            CreateMap<Category, GetAllCategoryDto>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName));
            
            CreateMap<AddCategoryCommandDto, Category>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName));

        }
    }
}
