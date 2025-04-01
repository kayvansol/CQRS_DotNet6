
namespace Store.IdentityServer
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>    
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
