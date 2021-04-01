using Microsoft.EntityFrameworkCore.Migrations;

namespace BookReview.Migrations
{
    public partial class FirstTabels2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BooksId",
                table: "Review",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Review_BooksId",
                table: "Review",
                column: "BooksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Books_BooksId",
                table: "Review",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Books_BooksId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_BooksId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "BooksId",
                table: "Review");
        }
    }
}
