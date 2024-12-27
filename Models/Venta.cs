using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudNet8MVC.Models;

public class Venta
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Monto { get; set; }
    public string Descripcion { get; set; }

    // Llave foránea para relacionar con Persona
    public int PersonaId { get; set; }
    
    [ForeignKey("ProductoId")]
    public Persona Persona { get; set; }
    public ICollection<Producto> Productos { get; set; }
   
}