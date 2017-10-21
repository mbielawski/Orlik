using System;
using AutoMapper;
using Orlik.Infrastructure.Services;
using Orlik.Web.ViewModels.Account;
using System.Web.Mvc;
using Orlik.Infrastructure.RequestModels;

namespace Orlik.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IResetPasswordService _resetPasswordService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IResetPasswordService resetPasswordService, IMapper mapper)
        {
            _userService = userService;
            _resetPasswordService = resetPasswordService;
            _mapper = mapper;
        }

        public ActionResult ChangePassword()
        {
            if (!IsUserSignedIn())
                return RedirectToAction("SignIn");

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangePassword(AccountChangePasswordViewModel vm)
        {
            if (!ModelState.IsValid)
                return Content("Coś poszło nie tak..");

            var user = _userService.GetUserByUsername(GetSignedInUsername());
            _userService.ChangePassword(user.Email, vm.NewPassword, vm.ActualPassword);

            return View("PasswordChanged");

        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ResetPassword(AccountResetPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
                return Content("Coś poszło nie tak...");

            var user = _userService.GetUserByEmail(vm.Email);
            if (user == null)
                return View("MessageSent");

            _resetPasswordService.Create(user);
            return View("MessageSent");
        }

        public ActionResult ResetPasswordConfirmed(Guid token)
        {
            var resetPassword = _resetPasswordService.GetByToken(token);
            if (resetPassword == null)
                throw new Exception("ResetPassword doesn't exists.");

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ResetPasswordConfirmed(AccountResetPasswordConfirmedViewModel vm)
        {
            if (!ModelState.IsValid)
                return Content("Coś poszło nie tak...");

            _userService.ChangePassword(vm.Email, vm.Password);

            var userId = _userService.GetIdByEmail(vm.Email);
            _resetPasswordService.Delete(userId);

            return View("PasswordChanged");
        }

        public ActionResult SignIn()
        {
            if (IsUserSignedIn())
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(AccountSignInViewModel vm)
        {
            if (!ModelState.IsValid)
                return Content("Coś poszło nie tak...");

            var userSignInRequestModel = _mapper.Map<AccountSignInViewModel, UserSignInRequestModel>(vm);

            if (!_userService.SignIn(userSignInRequestModel))
                return View("ActivateAccount");

            SetSignedInUser(vm.Username);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignOut()
        {
            if (IsUserSignedIn())
                return View();

            return RedirectToAction("SignIn");
        }

        public ActionResult SignOutConfirmed()
        {
            if (IsUserSignedIn())
                SignOutUser();

            return RedirectToAction("SignIn");
        }
    }
}