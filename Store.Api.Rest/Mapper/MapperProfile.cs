using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Store.Domain;
using Store.Domain.DTOs.Category;
using Store.Domain.DTOs.Customer;
using Store.Domain.DTOs.Product;

namespace Store.Api.Rest.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<Category, GetAllCategoryDto>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName));

            CreateMap<AddCategoryCommandDto, Category>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName));

            CreateMap<Product, GetAllProductDto>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));

            CreateMap<AddProductCommandDto, Product>();

            CreateMap<Customer, GetAllCustomerDto>();

            CreateMap<AddCustomerCommandDto, Customer>();

        }
    }
}
