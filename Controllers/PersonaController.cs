using CrudNet8MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrudNet8MVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : Controller
    {
        private static readonly List<Persona> Personas = new List<Persona>();

        // GET: /Persona/Dashboard
        [HttpGet("/Persona/Dashboard")]
        public IActionResult Dashboard()
        {
            // Pasa los datos necesarios a la vista si es necesario
            ViewData["Title"] = "Panel de Control de Personas";
            return View("Dashboard", Personas); // Asegúrate de que exista la vista Dashboard.cshtml en Views/Persona
        }

        // GET: /Persona/Create
        [HttpGet("/Persona/Crear")]
        public IActionResult Create()
        {
            // Retorna la vista Create.cshtml
            return View("Crear");
        }

        // POST: /Persona/Create
        [HttpPost("/Persona/Crear")]
        public IActionResult Create(Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return View("Crear", persona);
            }

            // Validar que el ID sea único
            if (Personas.Any(p => p.Id == persona.Id))
            {
                ModelState.AddModelError("Id", "Ya existe una persona con el mismo ID.");
                return View("Crear", persona);
            }

            // Sanitizar entradas sensibles
            persona.Nombre = Persona.SanitizarEntrada(persona.Nombre);
            persona.Apellido = Persona.SanitizarEntrada(persona.Apellido);
            persona.CorreoElectronico = Persona.SanitizarEntrada(persona.CorreoElectronico);
            persona.Direccion = Persona.SanitizarEntrada(persona.Direccion);

            Personas.Add(persona);

            // Redirigir al Dashboard tras crear la nueva persona
            return RedirectToAction("Dashboard");
        }

        // GET: api/persona
        [HttpGet]
        public ActionResult<IEnumerable<Persona>> GetAll()
        {
            return Ok(Personas);
        }

        // GET: api/persona/{id}
        [HttpGet("{id:int}")]
        public ActionResult<Persona> GetById(int id)
        {
            var persona = Personas.FirstOrDefault(p => p.Id == id);
            if (persona == null)
            {
                return NotFound(new { Message = "Persona no encontrada." });
            }
            return Ok(persona);
        }

        // POST: api/persona
        [HttpPost]
        public ActionResult CreateApi([FromBody] Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar que el ID sea único
            if (Personas.Any(p => p.Id == persona.Id))
            {
                return Conflict(new { Message = "Ya existe una persona con el mismo ID." });
            }

            // Sanitizar entradas sensibles
            persona.Nombre = Persona.SanitizarEntrada(persona.Nombre);
            persona.Apellido = Persona.SanitizarEntrada(persona.Apellido);
            persona.CorreoElectronico = Persona.SanitizarEntrada(persona.CorreoElectronico);
            persona.Direccion = Persona.SanitizarEntrada(persona.Direccion);

            Personas.Add(persona);
            return CreatedAtAction(nameof(GetById), new { id = persona.Id }, persona);
        }

        // PUT: api/persona/{id}
        [HttpPut("{id:int}")]
        public ActionResult Update(int id, [FromBody] Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPersona = Personas.FirstOrDefault(p => p.Id == id);
            if (existingPersona == null)
            {
                return NotFound(new { Message = "Persona no encontrada." });
            }

            // Actualizar datos sanitizados
            existingPersona.Nombre = Persona.SanitizarEntrada(persona.Nombre);
            existingPersona.Apellido = Persona.SanitizarEntrada(persona.Apellido);
            existingPersona.CorreoElectronico = Persona.SanitizarEntrada(persona.CorreoElectronico);
            existingPersona.NumeroTelefono = persona.NumeroTelefono; // Ya validado en el modelo
            existingPersona.FechaNacimiento = persona.FechaNacimiento;
            existingPersona.Direccion = Persona.SanitizarEntrada(persona.Direccion);

            return NoContent();
        }

        // DELETE: api/persona/{id}
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var persona = Personas.FirstOrDefault(p => p.Id == id);
            if (persona == null)
            {
                return NotFound(new { Message = "Persona no encontrada." });
            }

            Personas.Remove(persona);
            return NoContent();
        }
    }
}
