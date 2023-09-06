using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogEngine.Data.Migrations
{
    /// <inheritdoc />
    public partial class BlogEnginev4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "User",
                columns: new[] { "UserId", "Email", "LastName", "Name", "RoleId", "UserName" },
                values: new object[,]
                {
                    { 1, "nubem@gmail.com", "Mejia", "Nube", 1, "NubeM" },
                    { 2, "Juand@gmail.com", "Ojeda", "Juan", 2, "JuanD" },
                    { 3, "rayitoc@gmail.com", "Castro", "Rayuela", 3, "RayoC" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "User",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "User",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "User",
                keyColumn: "UserId",
                keyValue: 3);
        }
    }
}
