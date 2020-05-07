using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BKind.Models;
using BKind.Models.Manage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BKind.Controllers
{
    public class ManageController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<ManageController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly UrlEncoder _urlEncoder;

        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public ManageController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<ManageController> logger,
            IEmailSender emailSender,
            UrlEncoder urlEncoder)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            _urlEncoder = urlEncoder;
        }

        [ViewData]
        public bool RequirePassword { get; set; }

        [ViewData]
        public string SharedKey { get; set; }

        [ViewData]
        public string AuthenticatorUri { get; set; }

        [ViewData]
        public string Username { get; set; }

        [ViewData]
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        [ViewData]
        public IList<AuthenticationScheme> OtherLogins { get; set; }

        [ViewData]
        public bool ShowRemoveButton { get; set; }

        [ViewData]
        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult Notifications()
        {
            return View();
        }

     
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToAction("SetPassword");
            }

            var model = new ChangePassword();
            model.StatusMessage = StatusMessage;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Parola ta a fosț schimbată cu succes.";

            return RedirectToAction();
        }

        public async Task<IActionResult> DeletePersonalData()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);

            var model = new DeletePersonalData();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePersonalData(DeletePersonalData model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    ModelState.AddModelError(string.Empty, "Parolă incorectă.");
                    return View(model);
                }
            }

            var result = await _userManager.DeleteAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleteing user with ID '{userId}'.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }

       
        [HttpPost]
        public async Task<IActionResult> DownloadPersonalData(DownloadPersonalData model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            _logger.LogInformation("User with ID '{UserId}' asked for their personal data.", _userManager.GetUserId(User));

            // includerea datelor personale pentru a fi descarcate
            var personalData = new Dictionary<string, string>();
            var personalDataProps = typeof(AppUser).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in personalDataProps)
            {
                personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            }

            Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
            return new FileContentResult(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(personalData)), "text/json");
        }

       
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User); //utilizator curent
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var firstName = user.FirstName;
            var lastName = user.LastName;
            var dateOfBirth = user.DateOfBirth;
            var city = user.City;
            var country = user.Country;
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);


            var model = new Models.Manage.Index
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                City = city,
                Country = country,
                Email = email,
                PhoneNumber = phoneNumber
            };

            model.StatusMessage = StatusMessage;
            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Models.Manage.Index model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.DateOfBirth = model.DateOfBirth;
                user.City = model.City;
                user.Country = model.Country;
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction();
                }

                ModelState.AddModelError("", "User not updated, something went wrong.");

                return View(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Profilul tău a fost actualizat";
            return RedirectToAction();
        }


        public async Task<IActionResult> PersonalData()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new PersonalData();
            return View(model);
        }


        public async Task<IActionResult> SetPassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToAction("ChangePassword");
            }

            var model = new SetPassword();
            model.StatusMessage = StatusMessage;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SetPassword(SetPassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Parola ta a fost setată.";

            return RedirectToAction();
        }

       
    }
}