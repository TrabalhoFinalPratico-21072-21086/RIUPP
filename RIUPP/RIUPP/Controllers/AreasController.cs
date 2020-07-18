﻿using System;
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
    public class AreasController : Controller
    {
        /// <summary>
        /// Variável que identifica a Base de dados do projecto
        /// </summary>
        private readonly RIUPPDB _context;


        // Construtor
        public AreasController(RIUPPDB context)
        {
            _context = context;
        }

        // Retorna a vista com todas as Áreas
        // GET: Areas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Areas.ToListAsync());
        }

        // Mostra os detalhes da Área em questão
        // GET: Areas/Details/5
        public async Task<IActionResult> Details(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    /* Em SQL, a pesquisa seria esta:
                    Select * 
                    from Areas 
                    where Areas.Id = id 
                    */
                    var area = await _context.Areas.Include(f => f.Ficheiro)
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (area == null)
                    {
                        return NotFound();
                    }

                    return View(area);
                }
        /*

                // GET: Areas/Create
                public IActionResult Create()
                {
                    return View();
                }

                // POST: Areas/Create
                // To protect from overposting attacks, enable the specific properties you want to bind to, for 
                // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Create([Bind("Id,Nome,Designacao")] Area area)
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(area);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    return View(area);
                }

                // GET: Areas/Edit/5
                public async Task<IActionResult> Edit(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var area = await _context.Areas.FindAsync(id);
                    if (area == null)
                    {
                        return NotFound();
                    }
                    return View(area);
                }

                // POST: Areas/Edit/5
                // To protect from overposting attacks, enable the specific properties you want to bind to, for 
                // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Designacao")] Area area)
                {
                    if (id != area.Id)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(area);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!AreaExists(area.Id))
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
                    return View(area);
                }

                // GET: Areas/Delete/5
                public async Task<IActionResult> Delete(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var area = await _context.Areas
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (area == null)
                    {
                        return NotFound();
                    }

                    return View(area);
                }

                // POST: Areas/Delete/5
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> DeleteConfirmed(int id)
                {
                    var area = await _context.Areas.FindAsync(id);
                    _context.Areas.Remove(area);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                    return NotFound();
                }

                private bool AreaExists(int id)
                {
                    return _context.Areas.Any(e => e.Id == id);
                }*/
    }
}