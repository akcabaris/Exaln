using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Exaln.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIELTSReadingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "IELTS");

            migrationBuilder.CreateTable(
                name: "Exam",
                schema: "IELTS",
                columns: table => new
                {
                    ExamID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExamTypeEnumID = table.Column<short>(type: "smallint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.ExamID);
                });

            migrationBuilder.CreateTable(
                name: "ReadingSection",
                schema: "IELTS",
                columns: table => new
                {
                    ReadingSectionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExamID = table.Column<int>(type: "integer", nullable: true),
                    SectionNo = table.Column<short>(type: "smallint", nullable: true),
                    PassageHeader = table.Column<string>(type: "character varying(1200)", maxLength: 1200, nullable: true),
                    PassageText = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: true),
                    SectionExplanation = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingSection", x => x.ReadingSectionID);
                    table.ForeignKey(
                        name: "FK_ReadingSection_Exam_ExamID",
                        column: x => x.ExamID,
                        principalSchema: "IELTS",
                        principalTable: "Exam",
                        principalColumn: "ExamID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReadingSectionPart",
                schema: "IELTS",
                columns: table => new
                {
                    ReadingSectionPartID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReadingSectionID = table.Column<int>(type: "integer", nullable: true),
                    QuestionTypeEnumID = table.Column<short>(type: "smallint", nullable: true),
                    PartNo = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingSectionPart", x => x.ReadingSectionPartID);
                    table.ForeignKey(
                        name: "FK_ReadingSectionPart_ReadingSection_ReadingSectionID",
                        column: x => x.ReadingSectionID,
                        principalSchema: "IELTS",
                        principalTable: "ReadingSection",
                        principalColumn: "ReadingSectionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReadingQuestion",
                schema: "IELTS",
                columns: table => new
                {
                    ReadingQuestionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionNo = table.Column<short>(type: "smallint", nullable: true),
                    QuestionText = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Answer = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ReadingSectionPartID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingQuestion", x => x.ReadingQuestionID);
                    table.ForeignKey(
                        name: "FK_ReadingQuestion_ReadingSectionPart_ReadingSectionPartID",
                        column: x => x.ReadingSectionPartID,
                        principalSchema: "IELTS",
                        principalTable: "ReadingSectionPart",
                        principalColumn: "ReadingSectionPartID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReadingQuestion_ReadingSectionPartID",
                schema: "IELTS",
                table: "ReadingQuestion",
                column: "ReadingSectionPartID");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingSection_ExamID",
                schema: "IELTS",
                table: "ReadingSection",
                column: "ExamID");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingSectionPart_ReadingSectionID",
                schema: "IELTS",
                table: "ReadingSectionPart",
                column: "ReadingSectionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReadingQuestion",
                schema: "IELTS");

            migrationBuilder.DropTable(
                name: "ReadingSectionPart",
                schema: "IELTS");

            migrationBuilder.DropTable(
                name: "ReadingSection",
                schema: "IELTS");

            migrationBuilder.DropTable(
                name: "Exam",
                schema: "IELTS");
        }
    }
}
