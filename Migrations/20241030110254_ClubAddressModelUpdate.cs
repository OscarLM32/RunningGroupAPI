using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunningGroupAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class ClubAddressModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_Addresses_AddressId",
                table: "Clubs");

            migrationBuilder.DropIndex(
                name: "IX_Clubs_AddressId",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Clubs");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Clubs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Clubs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Clubs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Clubs");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Clubs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_AddressId",
                table: "Clubs",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_Addresses_AddressId",
                table: "Clubs",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
