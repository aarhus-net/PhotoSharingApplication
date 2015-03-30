using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PhotoSharingApplication.Models;

namespace PhotoSharingApplication.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Login model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError( "", "the User name or password is incorrect!" );
            }
            return View("Login", model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index","Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newUser = Membership.CreateUser(model.UserName, model.Password);
                    FormsAuthentication.SetAuthCookie(model.UserName,false);
                    return RedirectToAction("Index","Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("Registration Error", e.StatusCode.ToString() );
                }
            }
            return View("Register", model);
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess
        }

        public ActionResult ResetPassword(ManageMessageId? message)
        {
            if (message != null)
            {
                ViewBag.StatusMessage = "Your password has been changed";
                ViewBag.ReturnUrl = Url.Action("ResetPassword");
            }

            return View("ResetPassword");
        }

        [HttpPost]
        public ActionResult ResetPassword(LocalPassword model)
        {
            ViewBag.ReturnUrl = Url.Action("ResetPassword");
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded;

                try
                {
                    changePasswordSucceeded = Membership.Provider.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction( "ResetPassword", new { message = ManageMessageId.ChangePasswordSuccess } );
                }

                ModelState.AddModelError( "", "The current password is incorrect or the new password is invalid" );

            }

            return View("ResetPassword", model);
        }
    }
}