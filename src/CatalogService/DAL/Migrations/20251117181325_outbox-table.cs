using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
	/// <inheritdoc />
	public partial class outboxtable : Migration
	{
		/// <inheritdoc />
		protected override void Up (MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Outbox",
				columns: table => new
				{
					Id = table.Column<int>( type: "int", nullable: false )
						.Annotation( "SqlServer:Identity", "1, 1" ),
					Data = table.Column<string>( type: "nvarchar(max)", nullable: false ),
					IsProcessed = table.Column<bool>( type: "bit", nullable: false ),
					CreatedOnUTC = table.Column<DateTime>( type: "datetime2", nullable: false )
				},
				constraints: table =>
				{
					table.PrimaryKey( "PK_Outbox", x => x.Id );
				} );
		}

		/// <inheritdoc />
		protected override void Down (MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Outbox" );
		}
	}
}
