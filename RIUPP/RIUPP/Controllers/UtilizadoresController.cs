using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RIUPP.Data;
using RIUPP.Models;

namespace RIUPP.Controllers{
    public class UtilizadoresController : Controller{

        /// <summary>
        /// Variável que identifica a Base de dados do projecto
        /// </summary>
        private readonly RIUPPDB _context;
        private readonly UserManager<IdentityUser> _userManager;

        // Construtor
        public UtilizadoresController(RIUPPDB context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        /* 
         Select * 
         from Utilizadores
        */
        /// <summary>
        /// GET: Utilizadores
        /// </summary>
        [Authorize]
        public async Task<IActionResult> Index(){
            //verifica se o user tem a conta suspensa
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");

            var utili =  _context.Utilizadores.Include(f => f.Ficheiro).OrderByDescending(f => f.Ficheiro.Count);
            return View(await utili.ToListAsync());
        }

        /* 
         Select * 
         from Utilizadores
         where Utilizador.Id = id;
        */
        /// <summary>
        /// GET: Utilizadores/Details/5
        /// </summary>
        [Authorize]
        public async Task<IActionResult> Details(int? id){
            //verifica se o user tem a conta suspensa
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");

            if (id == null){
                return NotFound();
            }

            var utilizador = await _context.Utilizadores.Include(u => u.Ficheiro)
                                                        .ThenInclude(f => f.Area)
                                                        .FirstOrDefaultAsync(u => u.Id == id);
            var role = await _context.UserRoles.FirstOrDefaultAsync(r => r.UserId == utilizador.Aut);
            var nameRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == role.RoleId);
            ViewBag.Cargo = nameRole.Name;
            if (utilizador == null){
                return NotFound();
            }

            return View(utilizador);
        }

        /// <summary>
        /// GET: Utilizadores/Edit/5
        /// </summary>
        [Authorize]
        public async Task<IActionResult> Edit(int? id){
            //verifica se o user tem a conta suspensa
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");

            if (id == null){
                return NotFound();
            }

            var utilizador = await _context.Utilizadores.FindAsync(id);
            if (utilizador == null){
                return NotFound();
            }
            return View(utilizador);
        }

        /*
         * To protect from overposting attacks, enable the specific properties you want to bind to, for more details, see http://go.microsoft.com/fwlink/?LinkId=317598. 
         */
        /// <summary>
        /// POST: Utilizadores/Edit/5
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email")] Utilizador utilizador){
            //verifica se o user tem a conta suspensa
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");

            if (id != utilizador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utilizador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilizadorExists(utilizador.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(utilizador);
        }


        /// <summary>
        /// GET: Utilizadores/Delete/5
        /// </summary>
        [Authorize]
        public async Task<IActionResult> Delete(int? id){
            //verifica se o user tem a conta suspensa
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");

            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilizador == null)
            {
                return NotFound();
            }

            return View(utilizador);
        }


        /// <summary>
        /// POST: Utilizadores/Delete/5
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id){
            //verifica se o user tem a conta suspensa
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");

            var utilizador = await _context.Utilizadores.FindAsync(id);
            if (utilizador == null) return NotFound();
            _context.Utilizadores.Remove(utilizador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilizadorExists(int id)
        {
            return _context.Utilizadores.Any(e => e.Id == id);
        }


        /// <summary>
        /// GET: Utilizadores/Details/5
        /// Muda  o cargo a um utilizador
        /// </summary>
        [HttpPost, ActionName("MudarCargo")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> MudarCargo(int idUtil, String crg){
            //verifica se o user tem a conta suspensa
            var useri = await _userManager.GetUserAsync(User);
            var utili = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == useri.Id);
            if (utili.Suspenso) return View("Suspenso");
            //descobre o user e o role do utilizador em questão
            Utilizador util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Id == idUtil);
            if (util == null) return NotFound();
            var role = await _context.UserRoles.FirstOrDefaultAsync(r => r.UserId == util.Aut);
            var user = await _userManager.FindByIdAsync(util.Aut);

            //se for para subir o cargo mas o utilizador em questão ja for o cargo maximo, não acontece nada, se não, sobe um patamar(Anonimo,Funcionario, Gestor)
            if (crg == "sobe"){
                //sobe para funcionario
                if(role.RoleId == "3"){
                    await _userManager.RemoveFromRoleAsync(user, "Anonimo");
                    await _userManager.AddToRoleAsync(user, "Funcionario");
                }
                //sobe para gestor
                else if(role.RoleId == "2"){
                    await _userManager.RemoveFromRoleAsync(user, "Funcionario");
                    await _userManager.AddToRoleAsync(user, "Gestor");
                }
            }
            //se for para descer o cargo mas o utilizador em questão ja for o cargo minimo, não acontece nada, se não, desce um patamar(Anonimo,Funcionario, Gestor)
            else
            {
                //desce para anonimo
                if (role.RoleId == "2"){
                    await _userManager.RemoveFromRoleAsync(user, "Funcionario");
                    await _userManager.AddToRoleAsync(user, "Anonimo");
                }
                //desce para funcionario
                else if (role.RoleId == "1"){
                    await _userManager.RemoveFromRoleAsync(user, "Gestor");
                    await _userManager.AddToRoleAsync(user, "Funcionario");
                }
            }
            return Redirect("Details/" + idUtil);
        }


        /// <summary>
        /// GET: Utilizadores/Details/5
        /// suspende ou activa a conta a um utilizador
        /// </summary>
        [HttpPost, ActionName("Suspensao")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Suspensao(int idUtil, string sus){
            //verifica se o user tem a conta suspensa
            var user = await _userManager.GetUserAsync(User);
            var utili = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (utili.Suspenso) return View("Suspenso");
            Utilizador util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Id == idUtil);
            if (util == null) return NotFound();
            if (sus == "activa") util.Suspenso = false;
            else util.Suspenso = true;
            _context.Update(util);
            await _context.SaveChangesAsync();
            return Redirect("Details/" + idUtil);
        }
    }
}
        