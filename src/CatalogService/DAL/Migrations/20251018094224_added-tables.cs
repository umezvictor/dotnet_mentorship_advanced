using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
	/// <inheritdoc />
	public partial class addedtables : Migration
	{
		/// <inheritdoc />
		protected override void Up (MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Products_Category_CategoryId",
				table: "Products" );

			migrationBuilder.AddColumn<DateTime>(
				name: "CreatedAt",
				table: "Products",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime( 1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified ) );

			migrationBuilder.AddColumn<DateTime>(
				name: "UpdatedAt",
				table: "Products",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime( 1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified ) );

			migrationBuilder.AddColumn<DateTime>(
				name: "CreatedAt",
				table: "Category",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime( 1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified ) );

			migrationBuilder.AddColumn<DateTime>(
				name: "UpdatedAt",
				table: "Category",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime( 1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified ) );

			migrationBuilder.AddCheckConstraint(
				name: "CK_Amount_Positive",
				table: "Products",
				sql: "[Amount] > 0" );

			migrationBuilder.AddForeignKey(
				name: "FK_Products_Category_CategoryId",
				table: "Products",
				column: "CategoryId",
				principalTable: "Category",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict );
		}

		/// <inheritdoc />
		protected override void Down (MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Products_Category_CategoryId",
				table: "Products" );

			migrationBuilder.DropCheckConstraint(
				name: "CK_Amount_Positive",
				table: "Products" );

			migrationBuilder.DropColumn(
				name: "CreatedAt",
				table: "Products" );

			migrationBuilder.DropColumn(
				name: "UpdatedAt",
				table: "Products" );

			migrationBuilder.DropColumn(
				name: "CreatedAt",
				table: "Category" );

			migrationBuilder.DropColumn(
				name: "UpdatedAt",
				table: "Category" );

			migrationBuilder.AddForeignKey(
				name: "FK_Products_Category_CategoryId",
				table: "Products",
				column: "CategoryId",
				principalTable: "Category",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade );
		}
	}
}
