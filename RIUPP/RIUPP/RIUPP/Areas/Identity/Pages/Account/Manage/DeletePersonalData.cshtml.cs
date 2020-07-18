using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RIUPP.Data;
using RIUPP.Models;

namespace RIUPP.Areas.Identity.Pages.Account.Manage
{
    
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly RIUPPDB _context;
        public DeletePersonalDataModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            RIUPPDB context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return Page();
                }
            }

            //apagar todos os registos relacionados com o user em questao na base de dado _context

            var utilizador = await _context.Utilizadores.Include(u => u.Ficheiro).FirstOrDefaultAsync(m => m.Aut == user.Id);
            var ficheiros = await _context.Ficheiros.Include(f => f.Download).Include(f => f.Comentario).FirstOrDefaultAsync(f => f.Dono == utilizador.Id);
            var comentarios = await _context.Comentarios.FirstOrDefaultAsync(c => c.FicheiroFK == ficheiros.Id || c.QuemComentou == utilizador.Id);
            var downloads = await _context.Downloads.FirstOrDefaultAsync(d => d.FicheiroFK == ficheiros.Id || d.UtilizadorFK == utilizador.Id);

            if (utilizador.Ficheiro != null){
                foreach (var fich in utilizador.Ficheiro){
                    System.IO.File.Delete("./wwwroot/Documentos/" + fich.Local);
                }
            }

            if (ficheiros != null) _context.Remove(ficheiros);
            if (downloads != null) _context.Remove(downloads);
            if (comentarios != null) _context.Remove(comentarios);
            if (ficheiros != null) _context.Remove(ficheiros);

            _context.Utilizadores.Remove(utilizador);
            await _context.SaveChangesAsync();

            var result = await _userManager.DeleteAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }
    }
}
