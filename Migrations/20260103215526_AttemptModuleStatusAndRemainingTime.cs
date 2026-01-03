using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exaln.Migrations
{
    /// <inheritdoc />
    public partial class AttemptModuleStatusAndRemainingTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "ExamAttemptModuleStatusEnumID",
                schema: "IELTS",
                table: "ExamAttemptModule",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "remainingSeconds",
                schema: "IELTS",
                table: "ExamAttemptModule",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IELTSExamExamID",
                schema: "IELTS",
                table: "ExamAttempt",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamAttempt_IELTSExamExamID",
                schema: "IELTS",
                table: "ExamAttempt",
                column: "IELTSExamExamID");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamAttempt_Exam_IELTSExamExamID",
                schema: "IELTS",
                table: "ExamAttempt",
                column: "IELTSExamExamID",
                principalSchema: "IELTS",
                principalTable: "Exam",
                principalColumn: "ExamID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamAttempt_Exam_IELTSExamExamID",
                schema: "IELTS",
                table: "ExamAttempt");

            migrationBuilder.DropIndex(
                name: "IX_ExamAttempt_IELTSExamExamID",
                schema: "IELTS",
                table: "ExamAttempt");

            migrationBuilder.DropColumn(
                name: "ExamAttemptModuleStatusEnumID",
                schema: "IELTS",
                table: "ExamAttemptModule");

            migrationBuilder.DropColumn(
                name: "remainingSeconds",
                schema: "IELTS",
                table: "ExamAttemptModule");

            migrationBuilder.DropColumn(
                name: "IELTSExamExamID",
                schema: "IELTS",
                table: "ExamAttempt");
        }
    }
}
