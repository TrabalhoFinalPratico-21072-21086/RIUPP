using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RIUPP.Data;
using RIUPP.Models;

using System.Web;


namespace RIUPP.Controllers{
    public class MinhaPaginaController : Controller {

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
        public MinhaPaginaController(RIUPPDB context, UserManager<IdentityUser> userManager, IWebHostEnvironment caminho)
        {
            _context = context;
            _userManager = userManager;
            _caminho = caminho;
        }

        /*
         * Select * 
         * from AspNetUsers, Utilizadores, Area, Ficheiros
         * where AspNetUsers.Id = Utilizadores.Aut and Ficheiros.Dono = Utilizadores.Id and Ficheiros.AreaFK = Area.Id
         */
        /// <summary>
        /// Pagina index da secção "Minha Página"
        /// GET: MinhaPagina
        /// </summary>
        [Authorize]
        public async Task<IActionResult> Index(){
            //verifica se o user tem a conta suspensa
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");

            var rIUPPDB = _context.Ficheiros.Include(f => f.Area)
                                             .Include(f => f.Utilizador)
                                             .Where(f => f.Dono == util.Id);
            ViewBag.username = util.Nome;
            ViewBag.email = util.Email;
            return View(await rIUPPDB.ToListAsync());
        }


        /*
         * Select *
         * from Area,Comentario,Ficheiros,Utilizador
         * where Ficheiro.Id = id and Comentario.FicheiroFK = Ficheiro.Id and Utilizador.Id = Ficheiro.Dono
         */
        /// <summary>
        /// GET: MinhaPagina/Details/5
        /// </summary>
        [Authorize]
        public async Task<IActionResult> Details(int? id) {
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
                .Include(c => c.Comentario)
                .ThenInclude(f => f.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ficheiro == null)
            {
                return NotFound();
            }

            return View(ficheiro);
        }
        /// <summary>
        /// GET: MinhaPagina/Create
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Create()
        {
            ViewData["AreaFK"] = new SelectList(_context.Areas, "Id", "Nome");
            ViewData["Dono"] = new SelectList(_context.Utilizadores, "Id", "Id");
            return View();
        }

        /*
         * Insert into Ficheiro values(Titulo,Descricao,Observacao,Local,Tipo,DateUpload,Dono,AreaFK)
         * 
         * To protect from overposting attacks, enable the specific properties you want to bind to, for more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         */
        /// <summary>
        ///  Se o ficheiro a carregar for null retorna para o mesmo formulario, caso esteja tudo bem ele cria uma String única para o nome do ficheiro e carrega o mesmo para o servidor.
        ///  POST: MinhaPagina/Create
        ///  Faz upload do ficheiro carregado
        /// </summary>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descricao,Observacao,Local,Tipo,DateUpload,Dono,AreaFK")] Ficheiro ficheiro, IFormFile fich) {
            // vars. auxiliares
            string caminhoCompleto = "";
            bool haFicheiro = false;

            //coloca na data actual na entrada do ficheiro na base de dados
            ficheiro.DateUpload = DateTime.Now;

            //verifica se o user que está a fazer upload não tem a conta suspensa
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");
            
            //relaciona na base de dados o ficheiro ao utilizador que o criou
            ficheiro.Dono = util.Id;

            //se o ficheiro nao existir, retorna para a mesma view de onde foi carregado mas agora com uma mensagem de erro
            if (fich == null) {
                ViewBag.bad = "null";
                ViewData["AreaFK"] = new SelectList(_context.Areas, "Id", "Nome", ficheiro.AreaFK);
                return View(ficheiro);
            }
            //se o ficheiro existir
            else {
                //cria uma string unica para por como nome
                Guid g;
                g = Guid.NewGuid();
                //retira a extenção do ficheiro
                string extensao = Path.GetExtension(fich.FileName).ToLower();
                //concatena a string com a extenção do ficheiro para criar um novo nome
                string nome = g.ToString() + extensao;
                //coloca na variavel o nome completo do ficheiro em questão concatenado com o seu caminho(directorio)
                caminhoCompleto = Path.Combine(_caminho.WebRootPath, "Documentos", nome);
                //na entrada da base de dados, coloca no Local o caminho do ficheiro para futuros downloads
                ficheiro.Local = nome;
                //comfirma que há ficheiro
                haFicheiro = true;
            }
            if (ModelState.IsValid) {
                try
                {
                    _context.Add(ficheiro);
                    await _context.SaveChangesAsync();
                    // se há ficheiro, guarda-se o mesmo no disco rigido
                    if (haFicheiro) {
                        using var stream = new FileStream(caminhoCompleto, FileMode.Create);
                        await fich.CopyToAsync(stream);
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception) {
                    ViewBag.bad = "erro";
                }
            }
            ViewData["AreaFK"] = new SelectList(_context.Areas, "Id", "Nome", ficheiro.AreaFK);
            return View(ficheiro);
        }

        /*
         * Select*
         * from Ficheiro
         * where Ficheiro.Id = id
         */
        /// <summary>
        /// Retorna a View(edit) do qual o utilizador pretende mudar, id do ficheiro passado por parametro
        /// GET: MinhaPagina/Edit/5
        /// </summary>
        [Authorize]
        public async Task<IActionResult> Edit(int? id) {
            //verifica se o user tem a conta suspensa
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");

            if (id == null) {
                return NotFound();
            }

            var ficheiro = await _context.Ficheiros.FindAsync(id);
            if (ficheiro == null) {
                return NotFound();
            }

            ViewData["Local"] = ficheiro.Local;
            ViewData["AreaFK"] = new SelectList(_context.Areas, "Id", "Nome", ficheiro.AreaFK);
            return View(ficheiro);
        }


        /*
         * Update Ficheiro set (Id,Titulo,Descricao,Observacao,Local,Tipo,DateUpload,Dono,AreaFK)
         * where Ficheiro.Id = id
         * 
         * To protect from overposting attacks, enable the specific properties you want to bind to, for more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         */
        /// <summary>
        /// Apaga o ficheiro antigo (delete) , substitui este por um novo ficheiro carregado (create) da mesma maneira que o anterior. Na base de dados simplesmente dá update
        /// POST: MinhaPagina/Edit/5
        /// Faz upload do projecto editado e muda a linha correspondente na tabela Ficheiros
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,Observacao,Local,Tipo,DateUpload,Dono,AreaFK")] Ficheiro ficheiro, IFormFile fich) {
            if (id != ficheiro.Id) {
                return NotFound();
            }

            // vars. auxiliares
            string caminhoCompleto = "", ficheiroVelho = "";
            bool haFicheiro = false;

            //coloca na data actual na entrada do ficheiro na base de dados
            ficheiro.DateUpload = DateTime.Now;

            //verifica se o user que está a fazer upload não tem a conta suspensa
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");

            //relaciona na base de dados o ficheiro ao utilizador que o criou
            ficheiro.Dono = util.Id;
            
            // se o ficheiro nao existir, retorna para a mesma view de onde foi carregado mas agora com uma mensagem de erro
            if (fich == null) {
                ViewBag.bad = "null";
                ViewData["AreaFK"] = new SelectList(_context.Areas, "Id", "Nome", ficheiro.AreaFK);
                return View(ficheiro);
            }
            //se o ficheiro existir
            else{
                //cria uma string unica para por como nome
                Guid g;
                g = Guid.NewGuid();
                //retira a extenção do ficheiro
                string extensao = Path.GetExtension(fich.FileName).ToLower();
                //concatena a string com a extenção do ficheiro para criar um novo nome
                string nome = g.ToString() + extensao;
                //coloca na variavel o nome completo do ficheiro em questão concatenado com o seu caminho(directorio)
                caminhoCompleto = Path.Combine(_caminho.WebRootPath, "Documentos", nome);
                //na entrada da base de dados, coloca no Local o caminho do ficheiro para futuros downloads e na variavel ficheiroVelho o nome do antigo ficheiro para depois o mesmo ser apagado
                ficheiroVelho = ficheiro.Local;
                ficheiro.Local = nome;
                //confirma que há ficheiro
                haFicheiro = true;
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(ficheiro);
                    await _context.SaveChangesAsync();
                    if (haFicheiro) {
                        //se há ficheiro, apaga-se o velho e guarda-se o novo
                        System.IO.File.Delete("./wwwroot/Documentos/" + ficheiroVelho);
                        using var stream = new FileStream(caminhoCompleto, FileMode.Create);
                        await fich.CopyToAsync(stream);
                    }
                }
                catch (DbUpdateConcurrencyException) {
                    if (!FicheiroExists(ficheiro.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                catch (Exception){
                    ViewBag.bad = "erro";
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Local"] = ficheiro.Local;
            ViewData["AreaFK"] = new SelectList(_context.Areas, "Id", "Nome", ficheiro.AreaFK);
            return View(ficheiro);
        }

        /*
         * Select *
         * from Ficheiros, Area, Utilizador
         * where Ficheiro.Id = id and Area.Id = Ficheiro.AreaFK and Ficheiro.Dono = Utilizador.Id 
         */

        /// <summary>
        /// Retorna a vista do ficheiro em questão
        /// GET: MinhaPagina/Delete/5
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

        /*
         * Delete 
         * from Ficheiro
         * Where Ficheiro.Id = id
         */
        /// <summary>
        /// Apaga o ficheiro real no disco rigido do servidor "System.IO.File.Delete("./wwwroot/Documentos/" + ficheiro.Local);"
        /// POST: MinhaPagina/Delete/5
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id) {
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
            //apaga o ficheiro do disco rigido
            System.IO.File.Delete("./wwwroot/Documentos/" + ficheiro.Local);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FicheiroExists(int id) {
            return _context.Ficheiros.Any(e => e.Id == id);
        }


        /*
         *  Insert into Download values (Data,FicheiroFK,Utilizador)
         *  https://stackoverflow.com/questions/3604562/download-file-of-any-type-in-asp-net-mvc-using-fileresult
         */
        /// <summary>
        /// retorna o ficheiro para download, adicionando a tabela corrospondida o mesmo acontecimento 
        /// </summary>
        [HttpPost, ActionName("Download")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<FileResult> Download(String down, int fich){
            //guarda na base de dados uma referencia ao utilizador que fez o download
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

            //retorna o ficheiro para download
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
        [Authorize]
        public async Task<IActionResult> Comentar(int fich, String comentario) {
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
        Select *
        From Ficheiros,Download
        Where Download.FicheiroFK = Ficheiro.id and Ficheiro.Id = id
        */
        /// <summary>
        ///  GET: DownloadsPage
        ///  retorna todos os downloads relacionados aos ficheiros em questão
        /// </summary>
        [Authorize]
        public async Task<IActionResult> Downloads(int? id) {
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
        [Authorize]
        public async Task<IActionResult> EsconderComentario(int fich, int com) {
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
        [Authorize(Roles = "Funcionario,Gestor")]
        public async Task<IActionResult> apagarComentario(int fich, int com){
            //verifica se o user tem a conta suspensa
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            if (util.Suspenso) return View("Suspenso");

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
        [Authorize(Roles = "Funcionario,Gestor")]
        public async Task<IActionResult> apagarDownload(int fich, int down){
            Download downl = await _context.Downloads.FirstOrDefaultAsync(d => d.Id == down);
            _context.Downloads.Remove(downl);
            await _context.SaveChangesAsync();
            return Redirect("Downloads/" + fich);
        }
    }
}
