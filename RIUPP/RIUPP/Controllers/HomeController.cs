using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RIUPP.Data;
using RIUPP.Models;

namespace RIUPP.Controllers{
    public class HomeController : Controller{

        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        /// <summary>
        /// Variável que identifica a Base de dados do projecto
        /// </summary>
        private readonly RIUPPDB _context;

        // Construtor
        public HomeController(ILogger<HomeController> logger, RIUPPDB context, UserManager<IdentityUser> userManager){
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }
        // Retorna a vista inicial do Home
        public async Task<IActionResult> IndexAsync(){
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                Utilizador util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
                if (util == null)
                {
                    util = new Utilizador
                    {
                        Nome = user.UserName.Substring(0, user.UserName.IndexOf("@")),
                        Email = user.Email,
                        Aut = user.Id
                    };

                    await _userManager.AddToRoleAsync(user, "Anonimo");

                    _context.Utilizadores.Add(util);
                    await _context.SaveChangesAsync();
                }
            }
            return View(await _context.Ficheiros.ToListAsync());
            return View(await _context.Ficheiros.ToListAsync());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(){
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
