using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImdbSearchResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImdbId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ResultType = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImdbSearchResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YoutubeSearchResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    ResourceId = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YoutubeSearchResults", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImdbSearchResults_ImdbId",
                table: "ImdbSearchResults",
                column: "ImdbId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeSearchResults_ResourceId",
                table: "YoutubeSearchResults",
                column: "ResourceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeSearchResults_Title",
                table: "YoutubeSearchResults",
                column: "Title")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImdbSearchResults");

            migrationBuilder.DropTable(
                name: "YoutubeSearchResults");
        }
    }
}
