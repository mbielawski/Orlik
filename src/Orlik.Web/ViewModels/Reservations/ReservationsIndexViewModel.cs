using Orlik.Model.Domain;
using System;

namespace Orlik.Web.ViewModels.Reservations
{
    public class ReservationsIndexViewModel
    {
        public int ReservationId { get; set; }
        public User Owner { get; set; }
        public DateTime Date { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}