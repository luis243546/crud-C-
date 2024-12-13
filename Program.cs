using CrudNet8MVC.Datos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configuramos la conexión a sql ser local db MSSQLLOCAL
/*builder.Services.AddDbContext<ApplicationDbContext>(opciones => 
            opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionMySql")));*/
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
    opciones.UseMySql(
        builder.Configuration.GetConnectionString("ConexionMySql"),
        new MySqlServerVersion(new Version(8, 0, 40)) // Cambia según tu versión de MySQL
    ));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=Index}/{id?}");

app.Run();
