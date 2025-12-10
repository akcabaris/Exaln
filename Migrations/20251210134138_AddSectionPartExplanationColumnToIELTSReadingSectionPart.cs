using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exaln.Migrations
{
    /// <inheritdoc />
    public partial class AddSectionPartExplanationColumnToIELTSReadingSectionPart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SectionPartExplanation",
                schema: "IELTS",
                table: "ReadingSectionPart",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SectionPartExplanation",
                schema: "IELTS",
                table: "ReadingSectionPart");
        }
    }
}
