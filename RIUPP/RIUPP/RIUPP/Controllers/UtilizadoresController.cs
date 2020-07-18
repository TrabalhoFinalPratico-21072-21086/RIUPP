using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        // Construtor
        public UtilizadoresController(RIUPPDB context){
            _context = context;
        }


        /* 
         Select * 
         from Utilizadores
        */
        // GET: Utilizadores
        [Authorize]
        public async Task<IActionResult> Index(){
            var util =  _context.Utilizadores.Include(f => f.Ficheiro).OrderBy(f => f.Ficheiro.Count);
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
                                                        .FirstOrDefaultAsync(u => u.Id == id);
            if (utilizador == null){
                return NotFound();
            }

            return View(utilizador);
        }
        /*
        // GET: Utilizadores/Create
        [Authorize]
        public IActionResult Create(){
            //return View();
            return NotFound();
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
            //return View(utilizador);
            return NotFound();
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
            //return View(utilizador);
            return NotFound();
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

            //return View(utilizador);
            return NotFound();
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
            //return RedirectToAction(nameof(Index));
            return NotFound();
        }

        private bool UtilizadorExists(int id)
        {
            return _context.Utilizadores.Any(e => e.Id == id);
        }*/
    }
}
        