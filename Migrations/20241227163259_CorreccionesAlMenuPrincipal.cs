using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudNet8MVC.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionesAlMenuPrincipal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "Venta",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Venta");
        }
    }
}
