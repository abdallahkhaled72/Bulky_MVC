using BulkyBook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Areas.Identity.Pages.Account
{
    public class VerifyEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender; // Add this field

        public VerifyEmailModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender) // Add this parameter
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender; // Initialize the field
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string Email { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Verification Code")]
            public string Code { get; set; }

            [EmailAddress]
            public string Email { get; set; }
        }

        public IActionResult OnGet(string email, string returnUrl = null)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToPage("/Account/Login");
            }

            Email = email;
            ReturnUrl = returnUrl;
            Input = new InputModel { Email = email };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Retrieve the stored values from session
            var storedCode = HttpContext.Session.GetString("VerificationCode");
            var storedRealToken = HttpContext.Session.GetString("RealConfirmationToken");
            var storedUserId = HttpContext.Session.GetString("VerificationUserId");
            var storedExpiry = HttpContext.Session.GetString("VerificationCodeExpiry");

            // Check if code exists and is not expired
            if (string.IsNullOrEmpty(storedCode) || string.IsNullOrEmpty(storedRealToken) ||
                string.IsNullOrEmpty(storedUserId) || string.IsNullOrEmpty(storedExpiry))
            {
                ModelState.AddModelError(string.Empty, "Verification code has expired. Please request a new one.");
                return Page();
            }

            // Check expiry
            if (!DateTime.TryParse(storedExpiry, out var expiryTime) || DateTime.Now > expiryTime)
            {
                ModelState.AddModelError(string.Empty, "Verification code has expired. Please request a new one.");
                return Page();
            }

            // Verify the 6-digit code
            if (Input.Code != storedCode)
            {
                ModelState.AddModelError(string.Empty, "Invalid verification code. Please try again.");
                return Page();
            }

            var user = await _userManager.FindByIdAsync(storedUserId);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to verify email. Please try registering again.");
                return Page();
            }
            // Use the REAL confirmation token to confirm the email
            var result = await _userManager.ConfirmEmailAsync(user, storedRealToken);
            if (result.Succeeded)
            {
                // Clear the session values after successful verification
                HttpContext.Session.Remove("VerificationCode");
                HttpContext.Session.Remove("RealConfirmationToken");
                HttpContext.Session.Remove("VerificationUserId");
                HttpContext.Session.Remove("VerificationCodeExpiry");

                // Sign in the user
                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl);
            }
            // If we got here, something failed
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }


        public async Task<IActionResult> OnPostResendCodeAsync()
        {
            if (string.IsNullOrEmpty(Input.Email))
            {
                ModelState.AddModelError(string.Empty, "Email address is required.");
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that user doesn't exist
                return RedirectToPage("./VerifyEmail", new { email = Input.Email, resend = true });
            }

            // Generate new 6-digit code
            var random = new Random();
            var newVerificationCode = random.Next(100000, 999999).ToString();

            // Update session values
            HttpContext.Session.SetString("VerificationCode", newVerificationCode);
            HttpContext.Session.SetString("RealConfirmationToken", await _userManager.GenerateEmailConfirmationTokenAsync(user));
            HttpContext.Session.SetString("VerificationUserId", await _userManager.GetUserIdAsync(user));
            HttpContext.Session.SetString("VerificationCodeExpiry", DateTime.Now.AddMinutes(10).ToString());

            // Resend email
            var formattedCode = $"{newVerificationCode.Substring(0, 3)} {newVerificationCode.Substring(3)}";
            await _emailSender.SendEmailAsync(Input.Email, "New Verification Code",
                $"Your new verification code is: <strong>{formattedCode}</strong>. This code will expire in 10 minutes.");

            // Add success message
            TempData["ResendSuccess"] = true;
            return RedirectToPage("./VerifyEmail", new { email = Input.Email });
        }

    }
}