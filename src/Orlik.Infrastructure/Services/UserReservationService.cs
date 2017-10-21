using Orlik.Infrastructure.Repositories;
using Orlik.Model.Domain;

namespace Orlik.Infrastructure.Services
{
    public class UserReservationService : IUserReservationService
    {
        private readonly UserReservationRepository _userReservationRepository;

        public UserReservationService(UserReservationRepository userReservationRepository)
        {
            _userReservationRepository = userReservationRepository;
        }

        public void Create(User user, Reservation reservation)
        {
            var userReservation = new UserReservation(user, reservation);

            _userReservationRepository.Add(userReservation);
        }

        public int GetNumberOfPlayers(int reservationId) =>
            _userReservationRepository.GetByReservationId(reservationId).Count;
    }
}
