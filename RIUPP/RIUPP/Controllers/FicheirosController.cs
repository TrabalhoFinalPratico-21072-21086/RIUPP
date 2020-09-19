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
        /// Variável que identifica a Base de dados do projecto.
        /// </summary>
        private readonly RIUPPDB _context;

        /// <summary>
        /// Variável que identifica, dentro da base de dados a parte do utilizador.
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Variável que contém os dados do servidor, em particular onde estão estes ficheiros guardados, no disco rigido do servidor.
        /// </summary>
        private readonly IWebHostEnvironment _caminho;

        // Construtor
        public FicheirosController(RIUPPDB context, UserManager<IdentityUser> userManager, IWebHostEnvironment caminho)
        {
            _context = context;
            _userManager = userManager;
            _caminho = caminho;
        }


        /// <summary>
        /// Select * from Ficheiros
        /// GET: Ficheiros
        /// </summary>
        public async Task<IActionResult> Index(){
            if (User.Identity.IsAuthenticated){
                //verifica se o user tem a conta suspensa
                var user = await _userManager.GetUserAsync(User);
                var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
                if (util.Suspenso) return View("Suspenso");
            }
            var rIUPPDB = _context.Ficheiros.Include(f => f.Area).Include(f => f.Utilizador);
            return View(await rIUPPDB.ToListAsync());
        }

        /*
         *  Update Ficheiro, Area, Comentario, Utilizador set 
         *  where Ficheiro.Id = id and Ficheiro.AreaFK = Area.Id and Ficheiro.Dono = Utilizador.Id and Ficheiro.Id = Comentario.FicheiroFK
         */
        /// <summary>
        /// Muda o que quiser entre o nome do ficheiro, descrição...
        /// GET: Ficheiros/Details/5
        /// </summary>
        [Authorize]
        public async Task<IActionResult> Details(int? id){
            //verifica se o user tem a conta suspensa
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");

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


        /// <summary>
        /// GET: Ficheiros/Edit/5
        /// </summary>
        [Authorize]
        [Authorize(Roles = "Gestor,Funcionario")]
        public async Task<IActionResult> Edit(int? id){
            //verifica se o user tem a conta suspensa
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");

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

        /*
         * To protect from overposting attacks, enable the specific properties you want to bind to, for more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         */
        /// <summary>
        /// POST: Ficheiros/Edit/5
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Authorize(Roles = "Gestor,Funcionario")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,Observacao,Dono,Local,DateUpload,AreaFK")] Ficheiro ficheiro){
            //verifica se o user tem a conta suspensa
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");

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


        /// <summary>
        /// GET: Ficheiros/Delete/5
        /// </summary>
        [Authorize]
        [Authorize(Roles = "Gestor,Funcionario")]
        public async Task<IActionResult> Delete(int? id){
            //verifica se o user tem a conta suspensa
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");

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


        /// <summary>
        /// POST: Ficheiros/Delete/5
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Authorize(Roles = "Gestor,Funcionario")]
        public async Task<IActionResult> DeleteConfirmed(int id){
            //verifica se o user tem a conta suspensa
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");

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
         * Insert into Download values (Data,FicheiroFK,Utilizador)
         * https://stackoverflow.com/questions/3604562/download-file-of-any-type-in-asp-net-mvc-using-fileresult
         */
        /// <summary>
        /// retorna o ficheiro para download, adicionando a tabela corrospondida o mesmo acontecimento.
        /// </summary>
        [HttpPost, ActionName("Download")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<FileResult> Download(String down, int fich){
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
         * Insert into Comentario values(Coment,FicheiroFK,QuemComentou,Date)
         */
        /// <summary>
        /// Cria uma nova linha de comentario em relação ao ficheiro em questao
        /// </summary>
        [HttpPost, ActionName("Comentar")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Comentar(int fich, String comentario){
            //se o comentario for null ou string vazia, não guarda e retorna para a pagina do ficheiro que comentou
            if (comentario == "" || comentario == null) return Redirect("Details/" + fich);
            //Caso o user estiver suspenso retorna para a pagina de suspenso
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");
            //Cria um comentario
            Comentario coment = new Comentario
            {
                Coment = comentario,
                FicheiroFK = fich,
                QuemComentou = util.Id,
                Date = DateTime.Now,
                Visivel = true
            };
            //guarda na base de dados esse comentario
            _context.Comentarios.Add(coment);
            await _context.SaveChangesAsync();

            return Redirect("Details/" + fich);
        }


        /*
         * Select *
         * From Ficheiros,Download
         * Where Download.FicheiroFK = Ficheiro.id and Ficheiro.Id = id
         */
        /// <summary>
        /// GET: DownloadsPage
        /// Retorna todos os downloads relacionados aos ficheiros em questão
        /// </summary>
        [Authorize(Roles = "Gestor,Funcionario")]
        public async Task<IActionResult> Downloads(int? id){
            //verifica se o user tem a conta suspensa
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");

            var rIUPPDB = _context.Downloads.Include(d => d.Ficheiro)
                                            .Include(d => d.Utilizador)
                                            .Where(d => d.Ficheiro.Id == id);
            return View(await rIUPPDB.ToListAsync());
        }

        /// <summary>
        ///  POST: MinhaPagina/Details/5
        ///  Esconde um comentario, ou seja, outras pessoas a não ser o dono do project nao vao poder ver este comentario
        /// </summary>
        [HttpPost, ActionName("EsconderComentario")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor,Funcionario")]
        public async Task<IActionResult> EsconderComentario(int fich, int com)
        {
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");
            Comentario coment = await _context.Comentarios.FirstOrDefaultAsync(c => c.Id == com);
            coment.Visivel = !coment.Visivel;
            _context.Comentarios.Update(coment);
            await _context.SaveChangesAsync();
            return Redirect("Details/" + fich);
        }


        /// <summary>
        ///  POST: MinhaPagina/Details/5
        ///  Apaga o comentario passado por parametro(int com)
        /// </summary>
        [HttpPost, ActionName("apagarComentario")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor,Funcionario")]
        public async Task<IActionResult> apagarComentario(int fich, int com){
            Comentario coment = await _context.Comentarios.FirstOrDefaultAsync(c => c.Id == com);
            _context.Comentarios.Remove(coment);
            await _context.SaveChangesAsync();
            return Redirect("Details/" + fich);
        }


        /// <summary>
        ///  POST: MinhaPagina/Downloads/5
        ///  Apaga a entrada de download que alguem fez no ficheiro em questão
        /// </summary>
        [HttpPost, ActionName("apagarDownload")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor,Funcionario")]
        public async Task<IActionResult> apagarDownload(int fich, int down){
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");
            Download downl = await _context.Downloads.FirstOrDefaultAsync(d => d.Id == down);
            _context.Downloads.Remove(downl);
            await _context.SaveChangesAsync();
            return Redirect("Downloads/" + fich);
        }
    }
}
