using Orlik.Model.Domain;
using System;

namespace Orlik.Web.ViewModels.Reservations
{
    public class ReservationsDetailsViewModel
    {
        public int ReservationId { get; set; }
        public User Owner { get; set; }
        public DateTime Date { get; set; }
        public Difficulty Difficulty { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int NumberOfPlayers { get; set; }
    }
}