using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daynamicforms.Migrations
{
    /// <inheritdoc />
    public partial class addform3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "forms",
                columns: table => new
                {
                    FID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_forms", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "question",
                columns: table => new
                {
                    QID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    questions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    anstype = table.Column<int>(type: "int", nullable: true),
                    answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_question", x => x.QID);
                    table.ForeignKey(
                        name: "FK_question_forms_FormsId",
                        column: x => x.FormsId,
                        principalTable: "forms",
                        principalColumn: "FID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_question_FormsId",
                table: "question",
                column: "FormsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "question");

            migrationBuilder.DropTable(
                name: "forms");
        }
    }
}
