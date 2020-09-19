using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RIUPP.Data;

namespace RIUPP.Controllers
{
    public class ErroController : Controller
    {
        /// <summary>
        /// Variável que identifica a Base de dados do projecto
        /// </summary>
        private readonly RIUPPDB _context;
        /// <summary>
        /// Variável que identifica, dentro da base de dados a parte do utilizador.
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        public ErroController(RIUPPDB context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        /// <summary>
        /// Erro 500
        /// Quando há um erro interno no servidor
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> QuinhentosAsync(){
            if (User.Identity.IsAuthenticated)
            {
                //verifica se o user tem a conta suspensa
                var user = await _userManager.GetUserAsync(User);
                var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
                if (util.Suspenso) return View("Suspenso");
            }
            ViewBag.num = "500";
            ViewBag.mens = "Erro interno do servidor";
            return View("ErrorPage");
        }

        /// <summary>
        /// Erro 404
        /// Quando não encontra um pagina especifica
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> QuatrocentosequatroAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                //verifica se o user tem a conta suspensa
                var user = await _userManager.GetUserAsync(User);
                var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
                if (util.Suspenso) return View("Suspenso");
            }
            ViewBag.num = "404";
            ViewBag.mens = "Página não encontrada";
            return View("ErrorPage");
        }
    }
}
