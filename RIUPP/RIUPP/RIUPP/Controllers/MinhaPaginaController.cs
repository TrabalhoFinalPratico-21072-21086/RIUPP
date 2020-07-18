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


namespace RIUPP.Controllers
{
    public class MinhaPaginaController : Controller
    {
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
        Select *
        from AspNetUsers, Utilizadores, Area, Ficheiros
        where AspNetUsers.Id = Utilizadores.Aut and Ficheiros.Dono = Utilizadores.Id and Ficheiros.AreaFK = Area.Id
        */
        // GET: MinhaPagina
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            var rIUPPDB = _context.Ficheiros.Include(f => f.Area)
                                             .Include(f => f.Utilizador)
                                             .Where(f => f.Dono == util.Id);
            return View(await rIUPPDB.ToListAsync());
        }


        /*
        Select *
        from Area,Comentario,Ficheiros,Utilizador
        where Ficheiro.Id = id and Comentario.FicheiroFK = Ficheiro.Id and Utilizador.Id = Ficheiro.Dono
        */
        // GET: MinhaPagina/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
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



        /* 
        Retorna a View e envia todas as Áreas para a viewBag
        */
        // GET: MinhaPagina/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["AreaFK"] = new SelectList(_context.Areas, "Id", "Nome");
            //ViewData["Dono"] = new SelectList(_context.Utilizadores, "Id", "Id");
            return View();
        }


        /* 
        Insert into Ficheiro values(Titulo,Descricao,Observacao,Local,Tipo,DateUpload,Dono,AreaFK)
        Se o ficheiro a carregar for null retorna para o mesmo formulario, caso esteja tudo bem ele cria uma String única para o nome do ficheiro e carrega o mesmo para o servidor.
        */
        // POST: MinhaPagina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Faz upload do ficheiro carregado
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descricao,Observacao,Local,Tipo,DateUpload,Dono,AreaFK")] Ficheiro ficheiro, IFormFile fich) {
            string caminhoCompleto = "";
            bool haFicheiro = false;

            ficheiro.DateUpload = DateTime.Now;

            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            ficheiro.Dono = util.Id;

            if (fich == null) {
                ViewData["AreaFK"] = new SelectList(_context.Areas, "Id", "Nome", ficheiro.AreaFK);
                return View(ficheiro); 
            }
            else {
                Guid g;
                g = Guid.NewGuid();
                string extensao = Path.GetExtension(fich.FileName).ToLower();
                string nome = g.ToString() + extensao;
                caminhoCompleto = Path.Combine(_caminho.WebRootPath, "Documentos", nome);
                ficheiro.Local = nome;
                haFicheiro = true;
            }
            if (ModelState.IsValid) {
                try
                {
                    _context.Add(ficheiro);
                    await _context.SaveChangesAsync();
                    if (haFicheiro) {
                        using var stream = new FileStream(caminhoCompleto, FileMode.Create);
                        await fich.CopyToAsync(stream);
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception) { }
            }
            ViewData["AreaFK"] = new SelectList(_context.Areas, "Id", "Nome", ficheiro.AreaFK);
            return View(ficheiro);
        }


        /* 
        Retorna a View ficheiro que quer mudar
        Select * 
        from Ficheiro
        where Ficheiro.Id = id
        */
        // GET: MinhaPagina/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id) {
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
        Update Ficheiro set (Id,Titulo,Descricao,Observacao,Local,Tipo,DateUpload,Dono,AreaFK)
        where Ficheiro.Id = id
        Apaga o ficheiro antigo (delete) , substitui este por um novo ficheiro carregado (create) da mesma maneira que o anterior. Na base de dados simplesmente dá update
        */
        // POST: MinhaPagina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Faz upload do projecto editado e muda a linha correspondente na tabela Ficheiros
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,Observacao,Local,Tipo,DateUpload,Dono,AreaFK")] Ficheiro ficheiro, IFormFile fich)
        {
            if (id != ficheiro.Id) {
                return NotFound();
            }

            string caminhoCompleto = "", ficheiroVelho = "";
            bool haFicheiro = false;

            ficheiro.DateUpload = DateTime.Now;

            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            ficheiro.Dono = util.Id;
            if (fich == null) {
                ViewData["AreaFK"] = new SelectList(_context.Areas, "Id", "Nome", ficheiro.AreaFK);
                return View(ficheiro);
            }
            else {
                Guid g;
                g = Guid.NewGuid();
                string extensao = Path.GetExtension(fich.FileName).ToLower();
                string nome = g.ToString() + extensao;
                caminhoCompleto = Path.Combine(_caminho.WebRootPath, "Documentos", nome);
                ficheiroVelho = ficheiro.Local;
                ficheiro.Local = nome;
                haFicheiro = true;
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(ficheiro);
                    await _context.SaveChangesAsync();
                    if (haFicheiro) {
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
                return RedirectToAction(nameof(Index));
            }

            ViewData["Local"] = ficheiro.Local;
            ViewData["AreaFK"] = new SelectList(_context.Areas, "Id", "Nome", ficheiro.AreaFK);
            return View(ficheiro);
        }


        /* 
        Retorna a vista do ficheiro em questão
        Select *
        from Ficheiros, Area, Utilizador
        where Ficheiro.Id = id and Area.Id = Ficheiro.AreaFK and Ficheiro.Dono = Utilizador.Id
        */
        // GET: MinhaPagina/Delete/5
        [Authorize]
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


        /* 
        Delete 
        from Ficheiro
        Where Ficheiro.Id = id
        Apaga o ficheiro real no disco rigido do servidor "System.IO.File.Delete("./wwwroot/Documentos/" + ficheiro.Local);"
        */
        // POST: MinhaPagina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var ficheiro = await _context.Ficheiros.FindAsync(id);
            var comentarios = await _context.Comentarios.Include(c => c.Ficheiro)
                                                        .FirstOrDefaultAsync(c => c.FicheiroFK == ficheiro.Id);
            var downloads = await _context.Downloads.Include(d => d.Ficheiro)
                                                        .FirstOrDefaultAsync(d => d.FicheiroFK == ficheiro.Id);
            if(downloads != null)_context.Downloads.Remove(downloads);
            if (downloads != null)_context.Comentarios.Remove(comentarios);
            _context.Ficheiros.Remove(ficheiro);
            System.IO.File.Delete("./wwwroot/Documentos/" + ficheiro.Local);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FicheiroExists(int id){
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
        // Cria uma nova linha de comentario em relação ao ficheiro em questao
        [HttpPost, ActionName("Comentar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comentar(int fich, String comentario){
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Aut == user.Id);
            Comentario coment = new Comentario
            {
                Coment = comentario,
                FicheiroFK = fich,
                QuemComentou = util.Id,
                Date = DateTime.Now
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
        [Authorize]
        public async Task<IActionResult> Downloads(int? id){
            var rIUPPDB = _context.Downloads.Include(d => d.Ficheiro)
                                            .Include(d => d.Utilizador)
                                            .Where(d => d.Ficheiro.Id == id);
            return View(await rIUPPDB.ToListAsync());
        }

    }
}
