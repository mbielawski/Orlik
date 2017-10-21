using Orlik.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Orlik.Model.Domain
{
    public enum Difficulty
    {
        [Display(Name = "Łatwy")]
        Easy,
        [Display(Name = "Średni")]
        Medium,
        [Display(Name = "Trudny")]
        Hard
    }

    public class Reservation
    {
        public int ReservationId { get; protected set; }
        public User Owner { get; protected set; }
        public DateTime Date { get; protected set; }
        public Difficulty Difficulty { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public virtual IList<UserReservation> UsersReservation { get; set; }

        protected Reservation()
        {
        }

        public Reservation(DateTime date, User owner, Difficulty difficulty)
        {
            SetDate(date);
            SetOwner(owner);
            SetDifficulty(difficulty);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDate(DateTime date)
        {
            if (date.IsNotGreaterThanToday())
                throw new ArgumentException("Date must be greater than today");

            Date = date;
        }

        public void SetOwner(User owner)
        {
            Owner = owner;
        }

        public void SetDifficulty(Difficulty difficulty)
        {
            Difficulty = difficulty;
        }

        public void UpdateTimeNow()
            => UpdatedAt = DateTime.UtcNow;
    }
}
