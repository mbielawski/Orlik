using System;
using Orlik.Model.Domain;

namespace Orlik.Infrastructure.RequestModels
{
    public class ReservationCreateRequestModel
    {
        public DateTime Date { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
