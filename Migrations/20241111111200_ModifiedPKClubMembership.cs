using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunningGroupAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedPKClubMembership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_ClubMembership",
                table: "ClubMemberships",
                columns: new[] { "ClubId", "AppUserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClubMembership",
                table: "ClubMemberships");
        }
    }
}
