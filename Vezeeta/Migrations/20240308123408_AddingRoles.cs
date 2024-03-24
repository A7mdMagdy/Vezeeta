using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Vezeeta.Migrations
{
    /// <inheritdoc /> 
    public partial class AddingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "28cb07fe-3b0c-464c-ab82-69a71ae6b75d", null, "Admin", "admin" },
                    { "7f10b22f-4576-40dd-81b0-e1e1b94cae3d", null, "Doctor", "Doctor" },
                    { "eb45064f-5579-4600-b940-9d73d071036c", null, "Patient", "patient" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "28cb07fe-3b0c-464c-ab82-69a71ae6b75d");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "7f10b22f-4576-40dd-81b0-e1e1b94cae3d");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "eb45064f-5579-4600-b940-9d73d071036c");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
