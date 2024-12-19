using System.ComponentModel.DataAnnotations;


namespace CrudNet8MVC.Models;

public class Persona
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
    [RegularExpression(@"^[A-Za-záéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
    [RegularExpression(@"^[A-Za-záéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
    public string Apellido { get; set; }

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
    [StringLength(100, ErrorMessage = "El correo no puede tener más de 100 caracteres.")]
    public string CorreoElectronico { get; set; }

    [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
    [RegularExpression(@"^\d{9}$", ErrorMessage = "El número de teléfono debe contener exactamente 9 dígitos.")]
    public string NumeroTelefono { get; set; }

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    [DataType(DataType.Date, ErrorMessage = "La fecha de nacimiento no es válida.")]
    public DateTime FechaNacimiento { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    [StringLength(200, ErrorMessage = "La dirección no puede tener más de 200 caracteres.")]
    [RegularExpression(@"^[A-Za-z0-9áéíóúÁÉÍÓÚñÑ#,. ]+$", ErrorMessage = "La dirección contiene caracteres no válidos.")]
    public string Direccion { get; set; }

    // Método para calcular la edad basado en la fecha de nacimiento
    public int CalcularEdad()
    {
        var hoy = DateTime.Today;
        var edad = hoy.Year - FechaNacimiento.Year;
        if (FechaNacimiento.Date > hoy.AddYears(-edad)) edad--;
        return edad;
    }

    // Método estático para sanitizar entradas adicionales
    // public static string SanitizarEntrada(string entrada)
    // {
    //     if (string.IsNullOrWhiteSpace(entrada))
    //     {
    //         return string.Empty;
    //     }
    //
    //     // Eliminación de scripts y caracteres maliciosos
    //     entrada = Regex.Replace(entrada, @"<.*?>", string.Empty); // Eliminar etiquetas HTML
    //     entrada = Regex.Replace(entrada, @"['"";--]", string.Empty); // Evitar inyección SQL o comandos maliciosos
    //     return entrada.Trim();
    // }
    //
    // // Método para validar datos específicos antes de procesarlos
    // public static bool ValidarDatoPersonalizado(string dato, string patron)
    // {
    //     return Regex.IsMatch(dato, patron);
    // }
}