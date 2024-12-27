using CrudNet8MVC.Datos;
using CrudNet8MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CrudNet8MVC.Controllers
{
    public class VentaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VentaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Listar Ventas
        public async Task<IActionResult> VentaDashboard()
        {
            var ventas = _context.Venta.Include(v => v.Persona);
            return View(await ventas.ToListAsync());
        }

        // Detalles de una Venta
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null) return NotFound();

            var venta = await _context.Venta
                .Include(v => v.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (venta == null) return NotFound();

            return View(venta);
        }

        // Crear Venta (GET)
        public IActionResult Crear()
        {
            ViewBag.Personas = new SelectList(_context.Persona.ToList(), "Id", "Nombre");
            return View();
        }

        // Crear Venta (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Venta venta)
        {
            if (ModelState.IsValid)
            {
                // Verificar el PersonaId
                Console.WriteLine(venta.PersonaId); // Verifica que se reciba correctamente el valor.

                _context.Add(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(VentaDashboard));
            }

            ViewBag.Personas = new SelectList(_context.Persona.ToList(), "Id", "Nombre", venta.PersonaId);
            return View(venta);
        }

        // Editar Venta (GET)
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var venta = await _context.Venta.FindAsync(id);
            if (venta == null) return NotFound();

            ViewBag.Personas = new SelectList(_context.Persona.ToList(), "Id", "Nombre", venta.PersonaId);
            return View(venta);
        }

        // Editar Venta (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Venta venta)
        {
            if (id != venta.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.Id)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(VentaDashboard));
            }

            ViewBag.Personas = new SelectList(_context.Persona.ToList(), "Id", "Nombre", venta.PersonaId);
            return View(venta);
        }

        // Eliminar Venta (GET)
        public async Task<IActionResult> Borrar(int? id)
        {
            if (id == null) return NotFound();

            var venta = await _context.Venta
                .Include(v => v.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (venta == null) return NotFound();

            return View(venta);
        }

        // Eliminar Venta (POST)
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarConfirmado(int id)
        {
            var venta = await _context.Venta.FindAsync(id);
            if (venta != null)
            {
                _context.Venta.Remove(venta);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(VentaDashboard));
        }

        private bool VentaExists(int id)
        {
            return _context.Venta.Any(e => e.Id == id);
        }
    }
}
