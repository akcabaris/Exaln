using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Exaln.Migrations
{
    /// <inheritdoc />
    public partial class res : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "IELTS");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    TokenVersion = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "ExamAttempt",
                schema: "IELTS",
                columns: table => new
                {
                    ExamAttemptID = table.Column<Guid>(type: "uuid", nullable: false),
                    ExamID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    ExamStatusEnumID = table.Column<short>(type: "smallint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAttempt", x => x.ExamAttemptID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "ExamAttemptModule",
                schema: "IELTS",
                columns: table => new
                {
                    ExamAttemptModuleID = table.Column<Guid>(type: "uuid", nullable: false),
                    ExamAttemptID = table.Column<Guid>(type: "uuid", nullable: true),
                    ExamAttempModuleTypeID = table.Column<short>(type: "smallint", nullable: true),
                    RawScore = table.Column<int>(type: "integer", nullable: true),
                    BandScore = table.Column<decimal>(type: "numeric(3,1)", precision: 3, scale: 1, nullable: true),
                    ProgressJson = table.Column<string>(type: "jsonb", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAttemptModule", x => x.ExamAttemptModuleID);
                    table.ForeignKey(
                        name: "FK_ExamAttemptModule_ExamAttempt_ExamAttemptID",
                        column: x => x.ExamAttemptID,
                        principalSchema: "IELTS",
                        principalTable: "ExamAttempt",
                        principalColumn: "ExamAttemptID",
                        onDelete: ReferentialAction.Cascade);
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
                    PartNo = table.Column<short>(type: "smallint", nullable: true),
                    SectionPartExplanation = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "ExamAttemptReadingAnswer",
                schema: "IELTS",
                columns: table => new
                {
                    ExamAttemptReadingAnswerID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExamAttemptModuleID = table.Column<Guid>(type: "uuid", nullable: false),
                    ReadingQuestionID = table.Column<int>(type: "integer", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: false, defaultValue: ""),
                    AnsweredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAttemptReadingAnswer", x => x.ExamAttemptReadingAnswerID);
                    table.ForeignKey(
                        name: "FK_ExamAttemptReadingAnswer_ExamAttemptModule_ExamAttemptModul~",
                        column: x => x.ExamAttemptModuleID,
                        principalSchema: "IELTS",
                        principalTable: "ExamAttemptModule",
                        principalColumn: "ExamAttemptModuleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamAttemptReadingAnswer_ReadingQuestion_ReadingQuestionID",
                        column: x => x.ReadingQuestionID,
                        principalSchema: "IELTS",
                        principalTable: "ReadingQuestion",
                        principalColumn: "ReadingQuestionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamAttemptModule_ExamAttemptID",
                schema: "IELTS",
                table: "ExamAttemptModule",
                column: "ExamAttemptID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAttemptReadingAnswer_ExamAttemptModuleID_ReadingQuestio~",
                schema: "IELTS",
                table: "ExamAttemptReadingAnswer",
                columns: new[] { "ExamAttemptModuleID", "ReadingQuestionID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamAttemptReadingAnswer_ReadingQuestionID",
                schema: "IELTS",
                table: "ExamAttemptReadingAnswer",
                column: "ReadingQuestionID");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ExamAttemptReadingAnswer",
                schema: "IELTS");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ExamAttemptModule",
                schema: "IELTS");

            migrationBuilder.DropTable(
                name: "ReadingQuestion",
                schema: "IELTS");

            migrationBuilder.DropTable(
                name: "ExamAttempt",
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
