using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunningGroupAPI.Data.Migrations
{
	/// <inheritdoc />
	public partial class ClubAndMembershipCleaned : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_ClubMemberships_Clubs_ClubId",
				table: "ClubMemberships");

			migrationBuilder.DropPrimaryKey(
				name: "PK_ClubMemberships",
				table: "ClubMemberships");

			// Step 1: Add a new column of type string
			migrationBuilder.AddColumn<string>(
				name: "NewId",
				table: "Clubs",
				type: "nvarchar(450)",
				nullable: false,
				defaultValue: "");

			// Step 2: If desired, populate NewId with unique values like GUIDs
			migrationBuilder.Sql("UPDATE Clubs SET NewId = NEWID()");

			// Step 3: Drop any existing primary key constraint on the old column
			migrationBuilder.DropPrimaryKey(
				name: "PK_Clubs",
				table: "Clubs");

			// Step 4: Drop the old int column
			migrationBuilder.DropColumn(
				name: "Id",
				table: "Clubs");

			// Step 5: Rename NewId to Id
			migrationBuilder.RenameColumn(
				name: "NewId",
				table: "Clubs",
				newName: "Id");

			// Step 6: Add the primary key constraint back on the new Id column
			migrationBuilder.AddPrimaryKey(
				name: "PK_Clubs",
				table: "Clubs",
				column: "Id");


			migrationBuilder.AlterColumn<string>(
				name: "AppUserId",
				table: "ClubMemberships",
				type: "nvarchar(450)",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(450)")
				.Annotation("Relational:ColumnOrder", 1);

			migrationBuilder.AlterColumn<string>(
				name: "ClubId",
				table: "ClubMemberships",
				type: "nvarchar(450)",
				nullable: false,
				oldClrType: typeof(int),
				oldType: "int")
				.Annotation("Relational:ColumnOrder", 0);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			// Rollback by reversing these steps if desired
			migrationBuilder.DropPrimaryKey(
				name: "PK_Clubs",
				table: "Clubs");

			migrationBuilder.DropColumn(
				name: "Id",
				table: "Clubs");

			migrationBuilder.AddColumn<int>(
				name: "Id",
				table: "Clubs",
				type: "int",
				nullable: false)
				.Annotation("SqlServer:Identity", "1, 1");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Clubs",
				table: "Clubs",
				column: "Id");

			migrationBuilder.AlterColumn<string>(
				name: "AppUserId",
				table: "ClubMemberships",
				type: "nvarchar(450)",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(450)")
				.OldAnnotation("Relational:ColumnOrder", 1);

			migrationBuilder.AlterColumn<int>(
				name: "ClubId",
				table: "ClubMemberships",
				type: "int",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(450)")
				.OldAnnotation("Relational:ColumnOrder", 0);

			migrationBuilder.AddForeignKey(
				name: "FK_ClubMemberships_Clubs_ClubId",
				table: "ClubMemberships",
				column: "ClubId",
				principalTable: "Clubs",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
