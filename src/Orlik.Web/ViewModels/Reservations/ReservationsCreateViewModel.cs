using Orlik.Model.Domain;
using System;

namespace Orlik.Web.ViewModels.Reservations
{
    public class ReservationsCreateViewModel
    {
        public DateTime Date { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}