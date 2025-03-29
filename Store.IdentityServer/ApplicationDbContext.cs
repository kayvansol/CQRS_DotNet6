using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Store.IdentityServer
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>    
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
