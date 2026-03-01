using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseOnline.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseSessionStudentRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CourseSessionStudents_StudentId",
                table: "CourseSessionStudents",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseSessionStudents_StudentId",
                table: "CourseSessionStudents");
        }
    }
}
