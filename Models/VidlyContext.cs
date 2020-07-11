using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using VidlyStore.Models;

namespace VidlyStore.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    
    public class VidlyContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Customer> customers { get; set; }
        public DbSet<Movie> movie { get; set; }
        public DbSet<MemberShipType> memberShipTypes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<User> usersList{ get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public VidlyContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static VidlyContext Create()
        {
            return new VidlyContext();
        }
    }
}