using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using SQLitePCL;
using RIUPP.Data;
using Microsoft.EntityFrameworkCore;

namespace RIUPP.Areas.Identity.Pages.Account.Manage{
    public partial class EmailModel : PageModel{
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly RIUPPDB _context;

        public EmailModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender,
            RIUPPDB context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = context;
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel{
            [Required]
            [EmailAddress]
            [Display(Name = "Novo email")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(IdentityUser user){
            var email = await _userManager.GetEmailAsync(user);
            Email = email;

            Input = new InputModel{
                NewEmail = email,
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<IActionResult> OnGetAsync(){
            var user = await _userManager.GetUserAsync(User);
            if (user == null){
                return NotFound($"Não foi possível carregar o utilizador com o ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync(){
            var user = await _userManager.GetUserAsync(User);
            if (user == null){
                return NotFound($"Não foi possível carregar o utilizador com o ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid){
                await LoadAsync(user);
                return Page();
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.NewEmail != email){
                var userId = await _userManager.GetUserIdAsync(user);
                //caso o utilizador actualize o email, tambem vai actualizar na base de dados _context, na tabela Utilizadores a linha correspondente
                var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
                util.Email = Input.NewEmail;
                _context.Update(util);
                await _context.SaveChangesAsync();
                user.Email = Input.NewEmail;
                user.UserName = Input.NewEmail;
                await _userManager.UpdateAsync(user);
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);


                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { userId = userId, email = Input.NewEmail, code = code },
                    protocol: Request.Scheme);

                await _userManager.ConfirmEmailAsync(user, code);
                await _emailSender.SendEmailAsync(
                    Input.NewEmail,
                    "Confirma o teu email",
                    $"Porfavor confirma a tua conta <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicando aqui</a>.");

                StatusMessage = "Email de confirmação enviado. Por favor verifica o teu email.";
                return RedirectToPage();
            }

            StatusMessage = "Não foi possivel alterar o teu email.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync(){
            var user = await _userManager.GetUserAsync(User);
            if (user == null){
                return NotFound($"Não foi possível carregar o utilizador com o ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid){
                await LoadAsync(user);
                return Page();
            }

            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            util.Email = Input.NewEmail;
            _context.Update(util);
            await _context.SaveChangesAsync();
            user.Email = Input.NewEmail;
            await _userManager.UpdateAsync(user);

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirma o teu email",
                $"Porfavor confirma a tua conta <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicando aqui</a>.");

            StatusMessage = "Email de confirmação enviado. Por favor verifica o teu email.";
            return RedirectToPage();
        }
    }
}
