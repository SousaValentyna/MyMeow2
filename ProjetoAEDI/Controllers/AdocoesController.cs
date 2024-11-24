using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoAEDI.Data;
using ProjetoAEDI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAEDI.Controllers
{
    public class AdocoesController : Controller
    {
        private readonly AppDbContext _context;

        public AdocoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Adocoes
        public async Task<IActionResult> Index()
        {
            var adocoes = _context.Adocoes
                .Include(a => a.Gatinho)
                .Include(a => a.Adotante);
            return View(await adocoes.ToListAsync());
        }

        // GET: Adocoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adocao = await _context.Adocoes
                .Include(a => a.Gatinho)
                .Include(a => a.Adotante)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (adocao == null)
            {
                return NotFound();
            }

            return View(adocao);
        }

        // GET: Adocoes/Create
        public IActionResult Create()
        {
            // Carrega somente gatinhos que ainda não foram adotados
            ViewData["GatinhoId"] = new SelectList(_context.Gatinhos.Where(g => g.Adocao == null), "Id", "Nome");
            ViewData["AdotanteId"] = new SelectList(_context.Adotantes, "Id", "Nome");
            return View();
        }

        // POST: Adocoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GatinhoId,AdotanteId,DataAdocao")] Adocao adocao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adocao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Recarrega as listas em caso de erro
            ViewData["GatinhoId"] = new SelectList(_context.Gatinhos.Where(g => g.Adocao == null), "Id", "Nome", adocao.GatinhoId);
            ViewData["AdotanteId"] = new SelectList(_context.Adotantes, "Id", "Nome", adocao.AdotanteId);
            return View(adocao);
        }

        // GET: Adocoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adocao = await _context.Adocoes.FindAsync(id);
            if (adocao == null)
            {
                return NotFound();
            }

            ViewData["GatinhoId"] = new SelectList(_context.Gatinhos, "Id", "Nome", adocao.GatinhoId);
            ViewData["AdotanteId"] = new SelectList(_context.Adotantes, "Id", "Nome", adocao.AdotanteId);
            return View(adocao);
        }

        // POST: Adocoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GatinhoId,AdotanteId,DataAdocao")] Adocao adocao)
        {
            if (id != adocao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adocao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdocaoExists(adocao.Id))
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

            ViewData["GatinhoId"] = new SelectList(_context.Gatinhos, "Id", "Nome", adocao.GatinhoId);
            ViewData["AdotanteId"] = new SelectList(_context.Adotantes, "Id", "Nome", adocao.AdotanteId);
            return View(adocao);
        }

        // GET: Adocoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adocao = await _context.Adocoes
                .Include(a => a.Gatinho)
                .Include(a => a.Adotante)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (adocao == null)
            {
                return NotFound();
            }

            return View(adocao);
        }

        // POST: Adocoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adocao = await _context.Adocoes.FindAsync(id);
            if (adocao == null)
            {
                return NotFound(); // Evita a remoção de um objeto nulo
            }

            _context.Adocoes.Remove(adocao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método auxiliar para verificar a existência de uma adoção
        private bool AdocaoExists(int id)
        {
            return _context.Adocoes.Any(e => e.Id == id);
        }
    }
}
