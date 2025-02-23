using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.Domain;
using Store.Domain.DTOs.Customer;
using Store.Infra.Sql.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infra.Sql.Repositories.CustomerRepo
{
    public class CustomerRepository: Repository<Customer, int>, ICustomerRepository
    {

        private readonly StoreContext _context;
        private readonly IRepository<Customer, int> _repo;
        private readonly IMapper _mapper;

        public CustomerRepository(StoreContext context,
            IRepository<Customer, int> repo, IMapper mapper) : base(context)
        {
            _context = context;
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Customer> CreateAsync(Customer data)
        {
            _context.Add(data);
            _context.SaveChanges();

            return data;
        }

        public async Task<List<GetAllCustomerDto>> GetAllCustomersAsync()
        {
            var list = _repo.GetAll(predicate: x => x.Email != "");

            list = list.OrderByDescending(o => o.LastName);

            var result = _mapper.ProjectTo<GetAllCustomerDto>(list).ToList();

            return result;
        }
    }
}
