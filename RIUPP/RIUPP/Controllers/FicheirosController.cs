using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RIUPP.Data;
using RIUPP.Models;

namespace RIUPP.Controllers{
    public class FicheirosController : Controller{

        /// <summary>
        /// Variável que identifica a Base de dados do projecto
        /// </summary>
        private readonly RIUPPDB _context;
        /// <summary>
        /// Variável que identifica, dentro da base de dados a parte do utilizador
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;
        /// <summary>
        /// Variável que contém os dados do servidor, em particular onde estão estes ficheiros guardados, no disco rigido do servidor
        /// </summary>
        private readonly IWebHostEnvironment _caminho;

        // Construtor
        public FicheirosController(RIUPPDB context, UserManager<IdentityUser> userManager, IWebHostEnvironment caminho)
        {
            _context = context;
            _userManager = userManager;
            _caminho = caminho;
        }

        //Select * from Ficheiros
        // GET: Ficheiros
        public async Task<IActionResult> Index()
        {
            var rIUPPDB = _context.Ficheiros.Include(f => f.Area).Include(f => f.Utilizador);
            return View(await rIUPPDB.ToListAsync());
        }

        /* 
         Update Ficheiro, Area, Comentario, Utilizador set 
         Muda o que quiser entre o nome do ficheiro, descrição...
         where Ficheiro.Id = id and Ficheiro.AreaFK = Area.Id and Ficheiro.Dono = Utilizador.Id and Ficheiro.Id = Comentario.FicheiroFK
        */
        //GET: Ficheiros/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id) 
        {
            if (id == null) {
                return NotFound();
            }
            
            var ficheiro = await _context.Ficheiros
                .Include(f => f.Utilizador)
                .Include(f => f.Area)
                .Include(c => c.Comentario)
                .ThenInclude(f => f.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ficheiro == null){
                return NotFound();
            }

            return View(ficheiro);
        }

        // GET: Ficheiros/Edit/5
        [Authorize]
        [Authorize(Roles = "Gestor,Funcionario")]
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
            ViewData["AreaFK"] = new SelectList(_context.Areas, "Id", "Nome", ficheiro.AreaFK);
            ViewData["Dono"] = new SelectList(_context.Utilizadores, "Id", "Nome", ficheiro.Dono);
            return View(ficheiro);
        }

        // POST: Ficheiros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Authorize(Roles = "Gestor,Funcionario")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,Observacao,Dono,Local,DateUpload,AreaFK")] Ficheiro ficheiro){
            if (id != ficheiro.Id){
                return NotFound();
            }

            if (ModelState.IsValid){
                try{
                    _context.Update(ficheiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException){
                    if (!FicheiroExists(ficheiro.Id)){
                        return NotFound();
                    }
                    else{
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AreaFK"] = new SelectList(_context.Areas, "Id", "Nome", ficheiro.AreaFK);
            ViewData["Dono"] = new SelectList(_context.Utilizadores, "Id", "Nome", ficheiro.Dono);
            return View(ficheiro);
        }

        // GET: Ficheiros/Delete/5
        [Authorize]
        [Authorize(Roles = "Gestor,Funcionario")]
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
        [Authorize]
        [Authorize(Roles = "Gestor,Funcionario")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ficheiro = await _context.Ficheiros.FindAsync(id);
            var comentarios = await _context.Comentarios.Include(c => c.Ficheiro)
                                                        .FirstOrDefaultAsync(c => c.FicheiroFK == ficheiro.Id);
            var downloads = await _context.Downloads.Include(d => d.Ficheiro)
                                                        .FirstOrDefaultAsync(d => d.FicheiroFK == ficheiro.Id);
            if (downloads != null) _context.Downloads.Remove(downloads);
            if (downloads != null) _context.Comentarios.Remove(comentarios);
            _context.Ficheiros.Remove(ficheiro);
            System.IO.File.Delete("./wwwroot/Documentos/" + ficheiro.Local);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FicheiroExists(int id)
        {
            return _context.Ficheiros.Any(e => e.Id == id);
        }


        /* 
        Insert into Download values (Data,FicheiroFK,Utilizador)
        */
        //retorna o ficheiro para download, adicionando a tabela corrospondida o mesmo acontecimento
        //https://stackoverflow.com/questions/3604562/download-file-of-any-type-in-asp-net-mvc-using-fileresult
        [HttpPost, ActionName("Download")]
        [ValidateAntiForgeryToken]
        public async Task<FileResult> Download(String down, int fich)
        {
            var user = await _userManager.GetUserAsync(User);
            var utilizador = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            Download downl = new Download()
            {
                Data = DateTime.Now,
                FicheiroFK = fich,
                UtilizadorFK = utilizador.Id
            };
            _context.Downloads.Add(downl);
            await _context.SaveChangesAsync();

            byte[] fileBytes = System.IO.File.ReadAllBytes("./wwwroot/Documentos/" + down);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, down);
        }


        /* 
        Insert into Comentario values(Coment,FicheiroFK,QuemComentou,Date)
        */
        /// <summary>
        /// Cria uma nova linha de comentario em relação ao ficheiro em questao
        /// </summary>
        [HttpPost, ActionName("Comentar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comentar(int fich, String comentario)
        {
            if (comentario == "" || comentario == null) return Redirect("Details/" + fich);
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            Comentario coment = new Comentario
            {
                Coment = comentario,
                FicheiroFK = fich,
                QuemComentou = util.Id,
                Date = DateTime.Now,
                Visivel = true
            };
            _context.Comentarios.Add(coment);
            await _context.SaveChangesAsync();

            return Redirect("Details/" + fich);
        }



        /* 
        Select *
        From Ficheiros,Download
        Where Download.FicheiroFK = Ficheiro.id and Ficheiro.Id = id
        */
        // GET: DownloadsPage
        //retorna todos os downloads relacionados aos ficheiros em questão
        [Authorize(Roles = "Gestor,Funcionario")]
        public async Task<IActionResult> Downloads(int? id)
        {
            var rIUPPDB = _context.Downloads.Include(d => d.Ficheiro)
                                            .Include(d => d.Utilizador)
                                            .Where(d => d.Ficheiro.Id == id);
            return View(await rIUPPDB.ToListAsync());
        }


        [HttpPost, ActionName("EsconderComentario")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor,Funcionario")]
        public async Task<IActionResult> EsconderComentario(int fich, int com)
        {
            Comentario coment = await _context.Comentarios.FirstOrDefaultAsync(c => c.Id == com);
            coment.Visivel = !coment.Visivel;
            _context.Comentarios.Update(coment);
            await _context.SaveChangesAsync();
            return Redirect("Details/" + fich);
        }

        [HttpPost, ActionName("apagarComentario")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor,Funcionario")]
        public async Task<IActionResult> apagarComentario(int fich, int com){
            Comentario coment = await _context.Comentarios.FirstOrDefaultAsync(c => c.Id == com);
            _context.Comentarios.Remove(coment);
            await _context.SaveChangesAsync();
            return Redirect("Details/" + fich);
        }

        [HttpPost, ActionName("apagarDownload")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor,Funcionario")]
        public async Task<IActionResult> apagarDownload(int fich, int down){
            Download downl = await _context.Downloads.FirstOrDefaultAsync(d => d.Id == down);
            _context.Downloads.Remove(downl);
            await _context.SaveChangesAsync();
            return Redirect("Downloads/" + fich);
        }
    }
}
