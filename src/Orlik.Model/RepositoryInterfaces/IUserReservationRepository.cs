using Orlik.Model.Domain;
using System.Collections.Generic;

namespace Orlik.Model.RepositoryInterfaces
{
    public interface IUserReservationRepository : IRepository
    {
        void Add(UserReservation userReservation);
        IList<UserReservation> GetByReservationId(int id);
    }
}
