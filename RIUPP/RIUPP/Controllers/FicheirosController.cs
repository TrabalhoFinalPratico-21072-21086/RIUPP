using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RIUPP.Data;
using RIUPP.Models;

namespace RIUPP.Controllers
{
    public class FicheirosController : Controller
    {
        private readonly RIUPPDB _context;

        public FicheirosController(RIUPPDB context)
        {
            _context = context;
        }

        // GET: Ficheiros
        public async Task<IActionResult> Index()
        {
            var rIUPPDB = _context.Ficheiros.Include(f => f.Area).Include(f => f.Utilizador);
            return View(await rIUPPDB.ToListAsync());
        }

        // GET: Ficheiros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ficheiro = await _context.Ficheiros
                .Include(f => f.Area)
                .Include(f => f.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ficheiro == null)
            {
                return NotFound();
            }

            return View(ficheiro);
        }

        // GET: Ficheiros/Create
        public IActionResult Create()
        {
            ViewData["AreaFK"] = new SelectList(_context.Areas, "Id", "Id");
            ViewData["Dono"] = new SelectList(_context.Utilizadores, "Id", "Id");
            return View();
        }

        // POST: Ficheiros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descricao,Observacao,Local,Tipo,DateUpload,Dono,AreaFK")] Ficheiro ficheiro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ficheiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AreaFK"] = new SelectList(_context.Areas, "Id", "Id", ficheiro.AreaFK);
            ViewData["Dono"] = new SelectList(_context.Utilizadores, "Id", "Id", ficheiro.Dono);
            return View(ficheiro);
        }

        // GET: Ficheiros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ficheiro = await _context.Ficheiros.FindAsync(id);
            if (ficheiro == null)
            {
                return NotFound();
            }
            ViewData["AreaFK"] = new SelectList(_context.Areas, "Id", "Id", ficheiro.AreaFK);
            ViewData["Dono"] = new SelectList(_context.Utilizadores, "Id", "Id", ficheiro.Dono);
            return View(ficheiro);
        }

        // POST: Ficheiros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,Observacao,Local,Tipo,DateUpload,Dono,AreaFK")] Ficheiro ficheiro)
        {
            if (id != ficheiro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ficheiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FicheiroExists(ficheiro.Id))
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
            ViewData["AreaFK"] = new SelectList(_context.Areas, "Id", "Id", ficheiro.AreaFK);
            ViewData["Dono"] = new SelectList(_context.Utilizadores, "Id", "Id", ficheiro.Dono);
            return View(ficheiro);
        }

        // GET: Ficheiros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ficheiro = await _context.Ficheiros
                .Include(f => f.Area)
                .Include(f => f.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ficheiro == null)
            {
                return NotFound();
            }

            return View(ficheiro);
        }

        // POST: Ficheiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ficheiro = await _context.Ficheiros.FindAsync(id);
            _context.Ficheiros.Remove(ficheiro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FicheiroExists(int id)
        {
            return _context.Ficheiros.Any(e => e.Id == id);
        }
    }
}
