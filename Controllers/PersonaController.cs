using CrudNet8MVC.Datos;
using CrudNet8MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CrudNet8MVC.Controllers
{
    public class PersonaController : Controller
    {
       private readonly ApplicationDbContext _contexto;

        public PersonaController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }
        
       

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            return View(await _contexto.Persona.ToListAsync());
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Persona persona)
        {
            if (ModelState.IsValid)
            {
                _contexto.Persona.Add(persona);
                Console.WriteLine($"Datos de Persona agregados: {persona.Nombre} {persona.Apellido}");

                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }

            // Mostrar errores de validación
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Error: {error.ErrorMessage}");
            }

            return View(persona);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var persona = _contexto.Persona.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Persona persona)
        {
            if (ModelState.IsValid)
            {               
                _contexto.Update(persona);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = _contexto.Persona.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = _contexto.Persona.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarPersona(int? id)
        {
            var persona = await _contexto.Persona.FindAsync(id);
            if(persona == null)
            {
                return View();
            }

            //Borrado
            _contexto.Persona.Remove(persona);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Dashboard));            
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

