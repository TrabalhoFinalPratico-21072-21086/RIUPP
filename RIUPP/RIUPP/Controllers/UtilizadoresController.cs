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
        // GET: Utilizadores
        [Authorize]
        public async Task<IActionResult> Index(){
            var util =  _context.Utilizadores.Include(f => f.Ficheiro).OrderByDescending(f => f.Ficheiro.Count);
            return View(await util.ToListAsync());
        }

        /* 
         Select * 
         from Utilizadores
         where Utilizador.Id = id;
        */
        // GET: Utilizadores/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id){
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
        
        // GET: Utilizadores/Create
        [Authorize]
        public IActionResult Create(){
            return View();
        }

        // POST: Utilizadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email")] Utilizador utilizador){
            if (ModelState.IsValid){
                _context.Add(utilizador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //return View(utilizador);
            return NotFound();
        }

        // GET: Utilizadores/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id){
            if (id == null){
                return NotFound();
            }

            var utilizador = await _context.Utilizadores.FindAsync(id);
            if (utilizador == null){
                return NotFound();
            }
            return View(utilizador);
        }

        // POST: Utilizadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email")] Utilizador utilizador)
        {
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

        // GET: Utilizadores/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
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

        // POST: Utilizadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utilizador = await _context.Utilizadores.FindAsync(id);
            _context.Utilizadores.Remove(utilizador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilizadorExists(int id)
        {
            return _context.Utilizadores.Any(e => e.Id == id);
        }

        [HttpPost, ActionName("mudarCargo")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> mudarCargo(int idUtil, String crg){
            Utilizador util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Id == idUtil);
            var role = await _context.UserRoles.FirstOrDefaultAsync(r => r.UserId == util.Aut);
            var user = await _userManager.FindByIdAsync(util.Aut);
            if (crg == "sobe"){
                if(role.RoleId == "3"){
                    await _userManager.RemoveFromRoleAsync(user, "Anonimo");
                    await _userManager.AddToRoleAsync(user, "Funcionario");
                }
                else if(role.RoleId == "2"){
                    await _userManager.RemoveFromRoleAsync(user, "Funcionario");
                    await _userManager.AddToRoleAsync(user, "Gestor");
                }
            }
            else{
                if (role.RoleId == "2"){
                    await _userManager.RemoveFromRoleAsync(user, "Funcionario");
                    await _userManager.AddToRoleAsync(user, "Anonimo");
                }
                else if (role.RoleId == "1"){
                    await _userManager.RemoveFromRoleAsync(user, "Gestor");
                    await _userManager.AddToRoleAsync(user, "Funcionario");
                }
            }
            return Redirect("Details/" + idUtil);
        }
    }
}
        