using System;
using AutoMapper;
using Orlik.Infrastructure.Services;
using Orlik.Web.ViewModels.Users;
using System.Web.Mvc;
using Orlik.Infrastructure.RequestModels;

namespace Orlik.Web.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public ActionResult Register()
        {
            if (IsUserSignedIn())
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UsersRegisterViewModel vm)
        {
            if (!ModelState.IsValid)
                return Content("Coś poszło nie tak..");

            var userRegisterRequestModel = _mapper.Map<UsersRegisterViewModel, UserRegisterRequestModel>(vm);
            _userService.Register(userRegisterRequestModel);

            return View("VerificationEmailSent");
        }

        public ActionResult Activate(Guid activationId)
        {
            _userService.Activate(activationId);

            return View("AccountActivated");
        }
    }
}