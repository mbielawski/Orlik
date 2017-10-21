using AutoMapper;
using Orlik.Infrastructure.RequestModels;
using Orlik.Model.Domain;
using Orlik.Web.ViewModels.Account;
using Orlik.Web.ViewModels.Reservations;
using Orlik.Web.ViewModels.Users;

namespace Orlik.Web
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Reservation, ReservationsDetailsViewModel>();
                        cfg.CreateMap<ReservationsCreateViewModel, ReservationCreateRequestModel>();
                        cfg.CreateMap<UsersRegisterViewModel, UserRegisterRequestModel>();
                        cfg.CreateMap<AccountSignInViewModel, UserSignInRequestModel>();
                        cfg.CreateMap<Reservation, ReservationsIndexViewModel>();
                        cfg.CreateMap<Reservation, ReservationsEditViewModel>();
                    })
                    .CreateMapper();
        }
    }
}