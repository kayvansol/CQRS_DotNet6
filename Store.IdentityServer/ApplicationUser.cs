
namespace Store.IdentityServer
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(15)]
        public string Mobile { get; set; }
    }
}
