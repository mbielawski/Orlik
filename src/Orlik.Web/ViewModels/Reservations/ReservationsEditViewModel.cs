using System;
using System.Collections.Generic;
using Orlik.Model.Domain;

namespace Orlik.Web.ViewModels.Reservations
{
    public class ReservationsEditViewModel
    {
        public int ReservationId { get; set; }
        public DateTime Date { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}