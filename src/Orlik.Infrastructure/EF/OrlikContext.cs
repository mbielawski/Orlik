using Orlik.Model.Domain;
using System.Data.Entity;

namespace Orlik.Infrastructure.EF
{
    public class OrlikContext : DbContext
    {
        public OrlikContext() : base("OrlikContext")
        {
            Database.SetInitializer(new OrlikDbInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<UserReservation> UserReservations { get; set; }
        public DbSet<ResetPassword> ResetPasswords { get; set; }

        public virtual void Save()
            => SaveChanges();
    }
}
