using Orlik.Model.RepositoryInterfaces;
using Orlik.Model.Domain;
using Orlik.Infrastructure.EF;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Orlik.Infrastructure.Repositories
{
    public class UserReservationRepository : IUserReservationRepository
    {
        private readonly OrlikContext _context;

        public UserReservationRepository(OrlikContext context)
        {
            _context = context;
        }

        public void Add(UserReservation userReservation)
        {
            _context.UserReservations.Add(userReservation);
            _context.Save();
        }

        public IList<UserReservation> GetByReservationId(int id)
            => _context.UserReservations.Include(x => x.User).Where(x => x.ReservationId == id).ToList();
    }
}
