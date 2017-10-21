using System;
using Orlik.Model.Domain;
using System.Collections.Generic;
using Orlik.Infrastructure.RequestModels;

namespace Orlik.Infrastructure.Services
{
    public interface IReservationService : IService
    {
        void Create(ReservationCreateRequestModel reservationCreateDto, User owner);
        IList<Reservation> GetAll();
        IList<Reservation> GetReservationsOfUser(string username);
        Reservation GetById(int id);
        void Remove(int id, string signedInUsername);
        void Update(int id, DateTime date, Difficulty difficuly, string signedInUsername);
    }
}
