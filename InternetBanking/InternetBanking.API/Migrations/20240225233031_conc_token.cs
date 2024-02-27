using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetBanking.API.Migrations
{
    /// <inheritdoc />
    public partial class conc_token : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Clientes",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Clientes");
        }
    }
}
