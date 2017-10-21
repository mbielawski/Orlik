using Orlik.Model.Domain;

namespace Orlik.Infrastructure.Services
{
    public interface IUserReservationService : IService
    {
        void Create(User user, Reservation reservation);
        int GetNumberOfPlayers(int reservationId);
    }
}
