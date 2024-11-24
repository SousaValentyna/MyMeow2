using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoAEDI.Data;
using ProjetoAEDI.Models;

namespace ProjetoAEDI.Controllers
{
    public class AdotantesController : Controller
    {
        private readonly AppDbContext _context;

        public AdotantesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Adotantes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Adotantes.ToListAsync());
        }

        // GET: Adotantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adotante = await _context.Adotantes
                .Include(a => a.Adocoes) // Inclui as adoções relacionadas
                .ThenInclude(ad => ad.Gatinho) // Inclui os gatinhos adotados
                .FirstOrDefaultAsync(m => m.Id == id);

            if (adotante == null)
            {
                return NotFound();
            }

            return View(adotante);
        }

        // GET: Adotantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adotantes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Contato,Preferencias,HistoricoVisitas")] Adotante adotante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adotante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adotante);
        }

        // GET: Adotantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adotante = await _context.Adotantes.FindAsync(id);
            if (adotante == null)
            {
                return NotFound();
            }

            return View(adotante);
        }

        // POST: Adotantes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Contato,Preferencias,HistoricoVisitas")] Adotante adotante)
        {
            if (id != adotante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adotante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdotanteExists(adotante.Id))
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
            return View(adotante);
        }

        // GET: Adotantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adotante = await _context.Adotantes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (adotante == null)
            {
                return NotFound();
            }

            return View(adotante);
        }

        // POST: Adotantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adotante = await _context.Adotantes.FindAsync(id);
            if (adotante != null)
            {
                _context.Adotantes.Remove(adotante);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Método auxiliar para verificar a existência de um adotante
        private bool AdotanteExists(int id)
        {
            return _context.Adotantes.Any(e => e.Id == id);
        }
    }
}
