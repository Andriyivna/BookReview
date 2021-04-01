using Microsoft.EntityFrameworkCore.Migrations;

namespace BookReview.Migrations
{
    public partial class FirstTabels1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyShelf_Books_BooksID",
                table: "MyShelf");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "MyShelf");

            migrationBuilder.RenameColumn(
                name: "BooksID",
                table: "MyShelf",
                newName: "BooksId");

            migrationBuilder.RenameIndex(
                name: "IX_MyShelf_BooksID",
                table: "MyShelf",
                newName: "IX_MyShelf_BooksId");

            migrationBuilder.AlterColumn<int>(
                name: "BooksId",
                table: "MyShelf",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MyShelf_Books_BooksId",
                table: "MyShelf",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyShelf_Books_BooksId",
                table: "MyShelf");

            migrationBuilder.RenameColumn(
                name: "BooksId",
                table: "MyShelf",
                newName: "BooksID");

            migrationBuilder.RenameIndex(
                name: "IX_MyShelf_BooksId",
                table: "MyShelf",
                newName: "IX_MyShelf_BooksID");

            migrationBuilder.AlterColumn<int>(
                name: "BooksID",
                table: "MyShelf",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "MyShelf",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_MyShelf_Books_BooksID",
                table: "MyShelf",
                column: "BooksID",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
