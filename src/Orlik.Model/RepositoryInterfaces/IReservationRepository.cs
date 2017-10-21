using System.Collections.Generic;
using Orlik.Model.Domain;

namespace Orlik.Model.RepositoryInterfaces
{
    public interface IReservationRepository : IRepository
    {
        Reservation GetById(int? id);
        IList<Reservation> GetAll();
        void Add(Reservation reservation);
        void Update(Reservation reservation);
        void Remove(int id);
        IList<Reservation> GetReservationsOfUser(string username);
    }
}
