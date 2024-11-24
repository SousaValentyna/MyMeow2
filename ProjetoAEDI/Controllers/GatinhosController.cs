using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoAEDI.Data;
using ProjetoAEDI.Models;

namespace ProjetoAEDI.Controllers
{
    public class GatinhosController : Controller
    {
        private readonly AppDbContext _context;

        public GatinhosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Gatinhos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gatinhos.ToListAsync());
        }

        // GET: Gatinhos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gatinho = await _context.Gatinhos
                .Include(g => g.Adocao) // Inclui informações de adoção
                .FirstOrDefaultAsync(m => m.Id == id);

            if (gatinho == null)
            {
                return NotFound();
            }

            return View(gatinho);
        }

        // GET: Gatinhos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gatinhos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Idade,Cor,Temperamento,RequisitosEspeciais")] Gatinho gatinho)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gatinho);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gatinho);
        }

        // GET: Gatinhos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gatinho = await _context.Gatinhos.FindAsync(id);
            if (gatinho == null)
            {
                return NotFound();
            }

            return View(gatinho);
        }

        // POST: Gatinhos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Idade,Cor,Temperamento,RequisitosEspeciais")] Gatinho gatinho)
        {
            if (id != gatinho.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gatinho);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GatinhoExists(gatinho.Id))
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
            return View(gatinho);
        }

        // GET: Gatinhos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gatinho = await _context.Gatinhos
                .FirstOrDefaultAsync(m => m.Id == id);

            if (gatinho == null)
            {
                return NotFound();
            }

            return View(gatinho);
        }

        // POST: Gatinhos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gatinho = await _context.Gatinhos.FindAsync(id);
            if (gatinho != null)
            {
                _context.Gatinhos.Remove(gatinho);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool GatinhoExists(int id)
        {
            return _context.Gatinhos.Any(e => e.Id == id);
        }
    }
}
