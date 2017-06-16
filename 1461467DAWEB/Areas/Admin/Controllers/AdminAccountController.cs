using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using _1461467DAWEB.Models;
using Microsoft.Owin.Security;
using ShopConnection;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;

namespace _1461467DAWEB.Areas.Admin.Controllers
{
    [Authorize(Roles = "Manager, Admin")]
    public class AdminAccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        // GET: Admin/Account
        public ActionResult Index(int Page = 1)
        {
            var resultListAcount = Models.AspNetUsersModel.ListAccount(Page, 1);
            return View(resultListAcount);
        }


        // GET: Admin/Account/Create
        public ActionResult Create()
        {
            ViewBag.ChucVu = Models.AspNetUsersModel.ChucVu(User.Identity.GetUserId());
            return View();
        }

        public AdminAccountController()
        {
        }

        public AdminAccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Product");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return RedirectToAction("Index", "Product");
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>Create(RegisterViewModel model, String ChucVu)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new  UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber };
                var chkUser =   UserManager.Create(user, model.Password);
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, ChucVu);

                    return RedirectToAction("Index", "AdminAccount");
                }
                AddErrors(chkUser);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        // POST: /Account/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPasswordAdmin(ResetPasswordViewModel model, string MatKhauCu)
        {
            UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                Session["Messenger"] = "lỗi vui lòng kiểm tra lại !!";
                return RedirectToAction("ResetPassword");
            }
            else
            {
                var vc = Models.AspNetUsersModel.ChucVu(User.Identity.GetUserId());
                foreach (var item in vc)
                {
                    if (item.Email != user.Email)
                    {
                        Session["Messenger"] = "lỗi vui lòng kiểm tra lại !!";
                        return RedirectToAction("ResetPassword");
                    }
                }
                if (!UserManager.CheckPassword(user, MatKhauCu))
                {
                    Session["Messenger"] = "lỗi vui lòng kiểm tra lại !!";
                    return RedirectToAction("ResetPassword");
                }
                
                if(model.Password != model.ConfirmPassword)
                {
                    Session["Messenger"] = "lỗi vui lòng kiểm tra lại !!";
                    return RedirectToAction("ResetPassword");
                }

                UserManager.RemovePassword(user.Id);

                var result = UserManager.AddPassword(user.Id, model.Password);
                if (result.Succeeded)
                {
                    Session["Messenger"] = "Đổi mật khẩu thành công !!";
                    return RedirectToAction("ResetPasswordConfirmation");
                }
                AddErrors(result);
            }
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        public ActionResult ResetPassword()
        {
            return View();
        }
        

        // GET: Admin/Account/Edit/5
        public ActionResult Edit(String id)
        {
            String temp1 = "";
            String IdUser = "";
            ViewBag.ChucVu_Admin = Models.AspNetUsersModel.ChucVu(User.Identity.GetUserId());
            foreach (var i in ViewBag.ChucVu_Admin)
            {
                temp1 = i.Name;
                IdUser = i.Id;
            }
            String temp2 = "";
            ViewBag.ChucVu_Admin_2 = Models.AspNetUsersModel.ChucVu(id);
            foreach (var i in ViewBag.ChucVu_Admin_2)
            {
                temp2 = i.Name;
            }
            if(temp1 == temp2 && IdUser != id)
            {
                Session["Mes"] = "Bạn không thể sửa Thông tin user của chức vụ ngang bạn";
                return RedirectToAction("Index");
            }
            else if(temp1=="Admin" && temp2 == "Manager")
            {
                Session["Mes"] = "Bạn không thể sửa Thông tin user của chức vụ cao hơn bạn";
                return RedirectToAction("Index");
            }
            Session["ChucVu"] = temp1;
            return View();
        }

        // POST: Admin/Account/Edit/5

        [HttpPost, ActionName("Edit")]
        public async Task<ActionResult> Edit( RegisterViewModel model, String IdUser, String ChucVu)
        {

            AspNetRole Roles = Models.AspNetUsersModel.Roles(ChucVu);
            Models.AspNetUsersModel.UpdateRoles(IdUser,Roles.Id);

            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var  user = await UserManager.FindByIdAsync(IdUser);
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            UserManager.Update(user);

            await UserManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        // POST: /Users/Delete/5
        [HttpGet, ActionName("Delete")]
        public async Task<ActionResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
               

                ApplicationDbContext context = new ApplicationDbContext();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = await UserManager.FindByIdAsync(id);

                // lấy chức vụ của thằng đang đăng nhập vào admin và chức vụ của thằng bị xóa
                // nếu chức vụ của thằng đăng nhập == or nhỏ hơn chức vụ của thằng bị xóa thì không được xóa

                String temp1 = "";
                ViewBag.ChucVu_Admin = Models.AspNetUsersModel.ChucVu(User.Identity.GetUserId());
                foreach (var i in ViewBag.ChucVu_Admin)
                {
                    temp1 = i.Name;
                }
                String temp2 = "";
                ViewBag.ChucVu_Admin2 = Models.AspNetUsersModel.ChucVu(user.Id);
                foreach (var i in ViewBag.ChucVu_Admin2)
                {
                    temp2 = i.Name;
                }

                // không thể xóa chính người đang đăng nhập vào trang admin
                if (user.UserName == User.Identity.Name)
                {
                    Session["Mes"] = "Bạn không thể xóa chính bạn";
                }
                else if (temp1 == temp2)
                {
                    Session["Mes"] = "Bạn không thể xóa người có chức vụ ngang bạn";
                }else if (temp1 == "Admin" && temp2 ==  "Manager")
                {
                    Session["Mes"] = "Bạn không thể xóa người có chức vụ cao hơn bạn";
                }
                else
                {
                    var logins = user.Logins;
                    var rolesForUser = await UserManager.GetRolesAsync(id);

                    using (var transaction = context.Database.BeginTransaction())
                    {
                        foreach (var login in logins.ToList())
                        {
                            await UserManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                        }

                        if (rolesForUser.Count() > 0)
                        {
                            foreach (var item in rolesForUser.ToList())
                            {
                                // item should be the name of the role
                                var result = await UserManager.RemoveFromRoleAsync(user.Id, item);
                            }
                        }

                        await UserManager.DeleteAsync(user);
                        transaction.Commit();
                    }

                }
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    
        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    return View("ForgotPasswordConfirmation");
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "AdminAccount");
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}
