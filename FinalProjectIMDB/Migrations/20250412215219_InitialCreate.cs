using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectIMDB.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Title_Genres", x => x.GenreID);
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    titleID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    titleType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    primaryTitle = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    originalTitle = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    isAdult = table.Column<bool>(type: "bit", nullable: true),
                    startYear = table.Column<short>(type: "smallint", nullable: true),
                    endYear = table.Column<short>(type: "smallint", nullable: true),
                    runtimeMinutes = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.titleID);
                });

            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    titleID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    nameID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => new { x.titleID, x.nameID });
                    table.ForeignKey(
                        name: "FK_Directors_Titles1",
                        column: x => x.titleID,
                        principalTable: "Titles",
                        principalColumn: "titleID");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Directors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Titles");
        }
    }
}
