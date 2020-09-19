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

namespace RIUPP.Controllers
{
    public class AreasController : Controller
    {
        /// <summary>
        /// Variável que identifica a Base de dados do projecto
        /// </summary>
        private readonly RIUPPDB _context;

        /// <summary>
        /// Variável que identifica, dentro da base de dados a parte do utilizador.
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        // Construtor
        public AreasController(RIUPPDB context, UserManager<IdentityUser> userManager){
            _context = context;
            _userManager = userManager;
        }


        /// <summary>
        /// Retorna a vista com todas as Áreas, GET: Areas
        /// </summary>
        public async Task<IActionResult> Index(){
            if (User.Identity.IsAuthenticated){
                //verifica se o user tem a conta suspensa
                var user = await _userManager.GetUserAsync(User);
                var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
                if (util.Suspenso) return View("Suspenso");
            }
            return View(await _context.Areas.ToListAsync());
        }


        /// <summary>
        /// Mostra os detalhes da Área em questão, GET: Areas/Details/5
        /// </summary>
        public async Task<IActionResult> Details(int? id)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        //verifica se o user tem a conta suspensa
                        var user = await _userManager.GetUserAsync(User);
                        var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
                        if (util.Suspenso) return View("Suspenso");
                    }

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
        
                [Authorize(Roles = "Gestor")]
                /// <summary>
                /// GET: Areas/Create 
                /// </summary>
                public async Task<IActionResult> CreateAsync(){
                    //verifica se o user tem a conta suspensa
                    var user = await _userManager.GetUserAsync(User);
                    var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
                    if (util.Suspenso) return View("Suspenso");

                    return View();
                }
                

                /// <summary>
                /// POST: Areas/Create
                /// To protect from overposting attacks, enable the specific properties you want to bind to, for more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                /// </summary>
                [HttpPost]
                [ValidateAntiForgeryToken]
                [Authorize(Roles = "Gestor")]
                public async Task<IActionResult> Create([Bind("Id,Nome,Designacao")] Area area){
                    //verifica se o user tem a conta suspensa
                    var user = await _userManager.GetUserAsync(User);
                    var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
                    if (util.Suspenso) return View("Suspenso");

                    if (ModelState.IsValid)
                    {
                        _context.Add(area);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    return View(area);
                }

                /// <summary>
                /// GET: Areas/Edit/5
                /// </summary>
                [Authorize(Roles = "Gestor")]
                public async Task<IActionResult> Edit(int? id){
                    //verifica se o user tem a conta suspensa
                    var user = await _userManager.GetUserAsync(User);
                    var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
                    if (util.Suspenso) return View("Suspenso");

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

                /// <summary>
                /// POST: Areas/Edit/5
                /// To protect from overposting attacks, enable the specific properties you want to bind to, for more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                /// </summary>
                [HttpPost]
                [ValidateAntiForgeryToken]
                [Authorize(Roles = "Gestor")]
                public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Designacao")] Area area){
                    //verifica se o user tem a conta suspensa
                    var user = await _userManager.GetUserAsync(User);
                    var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
                    if (util.Suspenso) return View("Suspenso");

                    if (id != area.Id){
                        return NotFound();
                    }

                    if (ModelState.IsValid){
                        try{
                            _context.Update(area);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException){
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

                /// <summary>
                /// GET: Areas/Delete/5
                /// </summary>
                [Authorize(Roles = "Gestor")]
                public async Task<IActionResult> Delete(int? id){
                    //verifica se o user tem a conta suspensa
                    var user = await _userManager.GetUserAsync(User);
                    var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
                    if (util.Suspenso) return View("Suspenso");

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

                /// <summary>
                /// POST: Areas/Delete/5
                /// </summary>
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                [Authorize(Roles = "Gestor")]
                public async Task<IActionResult> DeleteConfirmed(int id){
                //verifica se o user tem a conta suspensa
                var user = await _userManager.GetUserAsync(User);
                var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
                if (util.Suspenso) return View("Suspenso");

                var area = await _context.Areas.FindAsync(id);
                    _context.Areas.Remove(area);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                private bool AreaExists(int id){
                    return _context.Areas.Any(e => e.Id == id);
                }
    }
}