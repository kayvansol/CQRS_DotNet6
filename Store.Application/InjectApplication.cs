using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Store.Application.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application
{
    public static class InjectApplication
    {
        public static void AddApplicationServicesRegister(this IServiceCollection service)
        {

            service.AddMediatR(Assembly.GetExecutingAssembly());

            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}
