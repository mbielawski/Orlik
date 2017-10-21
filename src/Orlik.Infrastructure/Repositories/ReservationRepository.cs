using System.Linq;
using Orlik.Model.Domain;
using Orlik.Infrastructure.EF;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using System.Data.Entity;
using Orlik.Model.RepositoryInterfaces;

namespace Orlik.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly OrlikContext _context;

        public ReservationRepository(OrlikContext context)
        {
            _context = context;
        }

        public void Add(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.Save();
        }

        public Reservation GetById(int? id)
            => _context.Reservations.Include(x => x.Owner).SingleOrDefault(x => x.ReservationId == id);

        public IList<Reservation> GetAll()
            => _context.Reservations.Include(x => x.Owner).ToList();

        public void Remove(int id)
        {
            var reservation = GetById(id);
            _context.Reservations.Remove(reservation);
            _context.Save();
        }

        public void Update(Reservation reservation)
        {
            reservation.UpdateTimeNow();
            _context.Reservations.AddOrUpdate(reservation);
            _context.Save();
        }

        public IList<Reservation> GetReservationsOfUser(string username)
            => _context.Reservations.Where(x => x.Owner.Username == username).ToList();
    }
}
