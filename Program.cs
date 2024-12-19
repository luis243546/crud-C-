using CrudNet8MVC.Datos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar la conexión a MySQL
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
    opciones.UseMySql(
        builder.Configuration.GetConnectionString("ConexionMySql"),
        new MySqlServerVersion(new Version(8, 0, 40)) // Cambia según tu versión de MySQL
    ));

// Agregar servicios al contenedor
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Configuración de rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "persona",
    pattern: "{controller=Persona}/{action=Dashboard}/{id?}");

app.MapControllerRoute(
    name: "inicio",
    pattern: "{controller=Inicio}/{action=Index}/{id?}");

app.Run();

