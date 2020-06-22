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
    public class DownloadsController : Controller
    {
        private readonly RIUPPDB _context;

        public DownloadsController(RIUPPDB context)
        {
            _context = context;
        }

        // GET: Downloads
        public async Task<IActionResult> Index()
        {
            var rIUPPDB = _context.Downloads.Include(d => d.Ficheiro).Include(d => d.Utilizador);
            return View(await rIUPPDB.ToListAsync());
        }

        // GET: Downloads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var download = await _context.Downloads
                .Include(d => d.Ficheiro)
                .Include(d => d.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (download == null)
            {
                return NotFound();
            }

            return View(download);
        }

        // GET: Downloads/Create
        public IActionResult Create()
        {
            ViewData["FicheiroFK"] = new SelectList(_context.Ficheiros, "Id", "Id");
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Id");
            return View();
        }

        // POST: Downloads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,FicheiroFK,UtilizadorFK")] Download download)
        {
            if (ModelState.IsValid)
            {
                _context.Add(download);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FicheiroFK"] = new SelectList(_context.Ficheiros, "Id", "Id", download.FicheiroFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Id", download.UtilizadorFK);
            return View(download);
        }

        // GET: Downloads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var download = await _context.Downloads.FindAsync(id);
            if (download == null)
            {
                return NotFound();
            }
            ViewData["FicheiroFK"] = new SelectList(_context.Ficheiros, "Id", "Id", download.FicheiroFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Id", download.UtilizadorFK);
            return View(download);
        }

        // POST: Downloads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,FicheiroFK,UtilizadorFK")] Download download)
        {
            if (id != download.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(download);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DownloadExists(download.Id))
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
            ViewData["FicheiroFK"] = new SelectList(_context.Ficheiros, "Id", "Id", download.FicheiroFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Id", download.UtilizadorFK);
            return View(download);
        }

        // GET: Downloads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var download = await _context.Downloads
                .Include(d => d.Ficheiro)
                .Include(d => d.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (download == null)
            {
                return NotFound();
            }

            return View(download);
        }

        // POST: Downloads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var download = await _context.Downloads.FindAsync(id);
            _context.Downloads.Remove(download);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DownloadExists(int id)
        {
            return _context.Downloads.Any(e => e.Id == id);
        }
    }
}
