using System.ComponentModel.DataAnnotations.Schema;

namespace CrudNet8MVC.Models;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public string Descripcion { get; set; }

    // Llave foránea para relacionar con Venta
    public int VentaId { get; set; }
    [ForeignKey("VentaId")]
    public Venta Venta { get; set; } 
}