# Dot Net & CQRS
<br />
CQRS Pattern & Clean Architecture Design In Dot Net 6.0 based Api Project with Test units in xUnit tool & Duende Identity Server & RabbitMQ
<br /><br />

# Project Structure :

![alt text](https://raw.githubusercontent.com/kayvansol/CQRS_DotNet6/refs/heads/main/img/structure.png?raw=true)

<hr />

# ERD :

![alt text](https://raw.githubusercontent.com/kayvansol/CQRS_DotNet6/refs/heads/main/img/ERD.png?raw=true)

<hr />

# Swagger page :

![alt text](https://raw.githubusercontent.com/kayvansol/CQRS_DotNet6/refs/heads/main/img/swagger.png?raw=true)

<hr />

# Order Command Handler Code for e.g. :

```
    public class AddOrderCommandDto
    {
        public int? CustomerId { get; set; }

        public byte OrderStatus { get; set; }

        public DateTime OrderDate { get; set; } 

        public DateTime RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public List<Items> Items { get; set; } = new List<Items>();

    }

    public class Items
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

    }
```

```
    public record AddOrderCommand(AddOrderCommandDto AddDto) : IRequest<ResultDto<Unit>>
    {

    }
```
```
    public class OrderRepository : Repository<Order, int>, IOrderRepository
    {

        private readonly StoreContext _context;
        private readonly IRepository<Order, int> _repo;
        private readonly IMapper _mapper;

        public OrderRepository(StoreContext context,
            IRepository<Order, int> repo, IMapper mapper) : base(context)
        {
            _context = context;
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Order> CreateAsync(Order data)
        {
            _context.Add(data);
            _context.SaveChanges();

            return data;
        }

    }
```

```
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, ResultDto<Unit>>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public AddOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public async Task<ResultDto<Unit>> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var dto = request.AddDto;

            List<Domain.OrderItem> orderItems = new List<Domain.OrderItem>();

            int i = 0;

            foreach (var item in dto.Items)
            {
                i++;

                orderItems.Add(new Domain.OrderItem
                {
                    Discount = item.Discount,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    ItemId = i                    
                });
            }

            Domain.Order order = new()
            {
                CustomerId = dto.CustomerId,
                OrderDate = dto.OrderDate,
                RequiredDate = dto.RequiredDate,
                ShippedDate = dto.ShippedDate,
                OrderStatus = dto.OrderStatus,
                OrderItems = orderItems
            };

            await orderRepository.CreateAsync(order);

            // Send order event success action message to RabbitMQ ...
            Producer.Produce($"Order with number {order.OrderId} created for customer number {order.CustomerId}");

            return ResultDto<Unit>.ReturnData(Unit.Value, (int)EnumResponseStatus.OK, (int)EnumResultCode.Success, EnumResultCode.Success.GetDisplayName());
        }
    }
```

<hr />

# Duende Identity Server :

![alt text](https://raw.githubusercontent.com/kayvansol/CQRS_DotNet6/refs/heads/main/img/identity.png?raw=true)

![alt text](https://raw.githubusercontent.com/kayvansol/CQRS_DotNet6/refs/heads/main/img/identity2.png?raw=true)

<hr />

# RabbitMQ with Docker :

```
docker pull rabbitmq:4.0.7-management

docker run -d --hostname myrabbit --name rabbit -p 5672:5672 -p 5673:5673 -p 15672:15672 rabbitmq:4.0.7-management
```
![alt text](https://raw.githubusercontent.com/kayvansol/CQRS_DotNet6/refs/heads/main/img/rabbitContainer.png?raw=true)

<hr />

# Order Message has been sent to rabbitmq :

![alt text](https://raw.githubusercontent.com/kayvansol/CQRS_DotNet6/refs/heads/main/img/rabbit.png?raw=true)

<hr />

# Hangfire dashbourd :

![alt text](https://raw.githubusercontent.com/kayvansol/CQRS_DotNet6/refs/heads/main/img/hangfire.png?raw=true)
