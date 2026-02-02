using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntityConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentsSubjects",
                table: "StudentsSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentsSubjects_StudID",
                table: "StudentsSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentsSubjects",
                table: "DepartmentsSubjects");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentsSubjects_DID",
                table: "DepartmentsSubjects");

            migrationBuilder.DropColumn(
                name: "StudSubID",
                table: "StudentsSubjects");

            migrationBuilder.DropColumn(
                name: "DeptSubID",
                table: "DepartmentsSubjects");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "DNameEn",
                table: "Departments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "DNameAr",
                table: "Departments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "InsManagerId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentsSubjects",
                table: "StudentsSubjects",
                columns: new[] { "StudID", "SubID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentsSubjects",
                table: "DepartmentsSubjects",
                columns: new[] { "DID", "SubID" });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    InsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuperVisorId = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.InsId);
                    table.ForeignKey(
                        name: "FK_Instructors_Departments_DID",
                        column: x => x.DID,
                        principalTable: "Departments",
                        principalColumn: "DID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instructors_Instructors_SuperVisorId",
                        column: x => x.SuperVisorId,
                        principalTable: "Instructors",
                        principalColumn: "InsId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstructorsSubjects",
                columns: table => new
                {
                    InsId = table.Column<int>(type: "int", nullable: false),
                    SubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorsSubjects", x => new { x.InsId, x.SubId });
                    table.ForeignKey(
                        name: "FK_InstructorsSubjects_Instructors_InsId",
                        column: x => x.InsId,
                        principalTable: "Instructors",
                        principalColumn: "InsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorsSubjects_Subjects_SubId",
                        column: x => x.SubId,
                        principalTable: "Subjects",
                        principalColumn: "SubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_InsManagerId",
                table: "Departments",
                column: "InsManagerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_DID",
                table: "Instructors",
                column: "DID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_SuperVisorId",
                table: "Instructors",
                column: "SuperVisorId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorsSubjects_SubId",
                table: "InstructorsSubjects",
                column: "SubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructors_InsManagerId",
                table: "Departments",
                column: "InsManagerId",
                principalTable: "Instructors",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructors_InsManagerId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "InstructorsSubjects");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentsSubjects",
                table: "StudentsSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentsSubjects",
                table: "DepartmentsSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Departments_InsManagerId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "InsManagerId",
                table: "Departments");

            migrationBuilder.AddColumn<int>(
                name: "StudSubID",
                table: "StudentsSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                table: "Students",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                table: "Students",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "DeptSubID",
                table: "DepartmentsSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "DNameEn",
                table: "Departments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "DNameAr",
                table: "Departments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentsSubjects",
                table: "StudentsSubjects",
                column: "StudSubID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentsSubjects",
                table: "DepartmentsSubjects",
                column: "DeptSubID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsSubjects_StudID",
                table: "StudentsSubjects",
                column: "StudID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsSubjects_DID",
                table: "DepartmentsSubjects",
                column: "DID");
        }
    }
}
