using System.Collections.Generic;
using Orlik.Model.Domain;
using System;
using Orlik.Common.Extensions;
using Orlik.Infrastructure.RequestModels;
using Orlik.Model.RepositoryInterfaces;

namespace Orlik.Infrastructure.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public void Create(ReservationCreateRequestModel reservationCreateRequestModel, User owner)
        {
            if (owner == null)
                throw new ArgumentNullException(nameof(owner));

            if (reservationCreateRequestModel == null)
                throw new ArgumentNullException(nameof(reservationCreateRequestModel));

            var reservation = new Reservation(reservationCreateRequestModel.Date, owner, reservationCreateRequestModel.Difficulty);

            _reservationRepository.Add(reservation);
        }

        public IList<Reservation> GetAll()
            => _reservationRepository.GetAll();

        public Reservation GetById(int id)
            => _reservationRepository.GetById(id);

        public IList<Reservation> GetReservationsOfUser(string username)
            => _reservationRepository.GetReservationsOfUser(username);

        public void Remove(int id, string signedInUsername)
        {
            var reservation = _reservationRepository.GetById(id);
            if (reservation == null)
                throw new Exception($"Reservation with id: {id} doesn't exists.");

            if (!reservation.Owner.Username.EqualsCaseInvariant(signedInUsername))
                throw new Exception($"Reservation with id: { id } has another owner.");

            _reservationRepository.Remove(id);
        }

        public void Update(int id, DateTime date, Difficulty difficulty, string signedInUsername)
        {
            var reservation = _reservationRepository.GetById(id);

            if (reservation == null)
                throw new Exception("Reservation doesn't exists.");

            if (date == DateTime.MinValue)
                throw new Exception("Date can not be empty.");

            if (!reservation.Owner.Username.EqualsCaseInvariant(signedInUsername))
                throw new Exception($"Reservation has another owner.");

            reservation.SetDate(date);
            reservation.SetDifficulty(difficulty);
            _reservationRepository.Update(reservation);
        }
    }
}
