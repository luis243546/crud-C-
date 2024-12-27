using CrudNet8MVC.Datos;
using CrudNet8MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CrudNet8MVC.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Listar Productos
        public async Task<IActionResult> ProductoDashboard()
        {
            var productos = _context.Producto.Include(p => p.Venta);
            return View(await productos.ToListAsync());
        }

        // Detalles de un Producto
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Producto
                .Include(p => p.Venta)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (producto == null) return NotFound();

            return View(producto);
        }

        // Crear Producto (GET)
        // public IActionResult Crear()
        // {
        //     ViewBag.Ventas = new SelectList(_context.Venta.ToList(), "Id", "Descripcion");
        //     return View();
        // }
        
        public IActionResult Crear()
        {
            var ventas = _context.Venta.ToList();

            // Si no hay ventas, pasar una lista vacía
            ViewData["Ventas"] = ventas.Any() ? ventas : new List<Venta>();

            return View();
        }


        // Crear Producto (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Producto producto)
        {
            if (ModelState.IsValid)
            { 
                // Verificar el VentaId
                Console.WriteLine(producto.VentaId); // Verifica que se reciba correctamente el valor.

                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ProductoDashboard));
            }

            ViewBag.Ventas = new SelectList(_context.Venta.ToList(), "Id", "Descripcion", producto.VentaId);
            return View(producto);
        }

        // Editar Producto (GET)
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null) return NotFound();

            ViewBag.Ventas = new SelectList(_context.Venta.ToList(), "Id", "Descripcion", producto.VentaId);
            return View(producto);
        } 

        // Editar Producto (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Producto producto)
        {
            if (id != producto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(ProductoDashboard));
            }

            ViewBag.Ventas = new SelectList(_context.Venta.ToList(), "Id", "Descripcion", producto.VentaId);
            return View(producto);
        }

        // Eliminar Producto (GET)
        public async Task<IActionResult> Borrar(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Producto
                .Include(p => p.Venta)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (producto == null) return NotFound();

            return View(producto);
        }

        // Eliminar Producto (POST)
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarConfirmado(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto != null)
            {
                _context.Producto.Remove(producto);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ProductoDashboard));
        }

        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.Id == id);
        }
    }
}
