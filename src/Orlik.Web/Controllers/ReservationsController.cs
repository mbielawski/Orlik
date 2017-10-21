using AutoMapper;
using Orlik.Infrastructure.Services;
using Orlik.Model.Domain;
using Orlik.Web.ViewModels.Reservations;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Orlik.Common.Extensions;
using Orlik.Infrastructure.RequestModels;

namespace Orlik.Web.Controllers
{
    public class ReservationsController : BaseController
    {
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;
        private readonly IUserReservationService _userReservationService;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationService reservationService,
                                      IUserService userService,
                                      IUserReservationService userReservationService,
                                      IMapper mapper)
        {
            _reservationService = reservationService;
            _userService = userService;
            _userReservationService = userReservationService;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            if (!IsUserSignedIn())
                return RedirectToAction("SignIn", "Account");

            var reservations = _reservationService.GetAll();
            var vm = _mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationsIndexViewModel>>(reservations);
            return View(vm);
        }

        public ActionResult Create()
        {
            if (IsUserSignedIn())
                return View();

            return RedirectToAction("SignIn", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservationsCreateViewModel vm)
        {
            if (!ModelState.IsValid)
                return Content("Coś poszło nie tak..");

            var owner = _userService.GetUserByUsername(GetSignedInUsername());
            var reservationCreateRequestModel = _mapper.Map<ReservationsCreateViewModel, ReservationCreateRequestModel>(vm);

            _reservationService.Create(reservationCreateRequestModel, owner);

            return View("ReservationAdded");
        }

        public new ActionResult User()
        {
            if (IsUserSignedIn())
                return View(_reservationService.GetReservationsOfUser(GetSignedInUsername()));

            return RedirectToAction("SignIn", "Account");
        }

        public ActionResult Details(int? id)
        {
            if (!IsUserSignedIn())
                return RedirectToAction("SignIn", "Account");

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var reservation = _reservationService.GetById(id.Value);
            if (reservation == null)
                return HttpNotFound();

            var numberOfPlayers = _userReservationService.GetNumberOfPlayers(id.Value);

            var vm = _mapper.Map<Reservation, ReservationsDetailsViewModel>(reservation);
            vm.NumberOfPlayers = numberOfPlayers;

            return View(vm);
        }

        public ActionResult Delete(int? id)
        {
            if (!IsUserSignedIn())
                return RedirectToAction("SignIn", "Account");

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var reservation = _reservationService.GetById(id.Value);
            if (reservation == null)
                return HttpNotFound();

            if (!reservation.Owner.Username.EqualsCaseInvariant(GetSignedInUsername()))
                return RedirectToAction("Index");

            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!IsUserSignedIn())
                return RedirectToAction("SignIn", "Account");

            _reservationService.Remove(id, GetSignedInUsername());

            return RedirectToAction("Index", "Reservations");
        }

        public ActionResult Edit(int? id)
        {
            if (!IsUserSignedIn())
                return RedirectToAction("SignIn", "Account");

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var reservation = _reservationService.GetById(id.Value);
            if (reservation == null)
                return HttpNotFound();

            if (!reservation.Owner.Username.EqualsCaseInvariant(GetSignedInUsername()))
                return RedirectToAction("Index");

            var vm = _mapper.Map<Reservation, ReservationsEditViewModel>(reservation);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReservationsEditViewModel vm)
        {
            if (!IsUserSignedIn())
                return RedirectToAction("SignIn", "Account");

            if (!ModelState.IsValid)
                return Content("Coś poszło nie tak..");

            _reservationService.Update(vm.ReservationId, vm.Date, vm.Difficulty, GetSignedInUsername());

            return View("ReservationEdited");
        }

        public ActionResult AddPlayer(int? id)
        {
            if (!IsUserSignedIn())
                return RedirectToAction("SignIn", "Account");

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var reservation = _reservationService.GetById(id.Value);
            var user = _userService.GetUserByUsername(GetSignedInUsername());

            _userReservationService.Create(user, reservation);
            return View("PlayerAdded");
        }
    }
}