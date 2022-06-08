using Microsoft.EntityFrameworkCore.Migrations;

namespace Jokes.Data.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LikedAlready",
                table: "UserLikedJokes",
                newName: "Liked");

            migrationBuilder.RenameColumn(
                name: "Riddle",
                table: "Jokes",
                newName: "Setup");

            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "Jokes",
                newName: "Punchline");

            migrationBuilder.AddColumn<int>(
                name: "OriginalId",
                table: "Jokes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalId",
                table: "Jokes");

            migrationBuilder.RenameColumn(
                name: "Liked",
                table: "UserLikedJokes",
                newName: "LikedAlready");

            migrationBuilder.RenameColumn(
                name: "Setup",
                table: "Jokes",
                newName: "Riddle");

            migrationBuilder.RenameColumn(
                name: "Punchline",
                table: "Jokes",
                newName: "Answer");
        }
    }
}
