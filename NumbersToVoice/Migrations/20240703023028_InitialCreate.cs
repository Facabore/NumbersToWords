using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NumbersToVoice.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    idUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nameUser = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    passwordUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emailUser = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.idUser);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_emailUser",
                table: "users",
                column: "emailUser",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
