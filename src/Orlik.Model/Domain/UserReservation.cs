using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orlik.Model.Domain
{
    public class UserReservation
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }
        [Key, Column(Order = 1)]
        public int ReservationId { get; set; }

        public virtual User User { get; protected set; }
        public virtual Reservation Reservation { get; protected set; }

        protected UserReservation()
        {
        }

        public UserReservation(User user, Reservation reservation)
        {
            SetUser(user);
            SetReservation(reservation);
        }

        public void SetUser(User user)
        {
            User = user ?? throw new ArgumentException("User can not be null.");
        }

        public void SetReservation(Reservation reservation)
        {
            Reservation = reservation ?? throw new ArgumentException("Reservation can not be null.");
        }
    }
}
