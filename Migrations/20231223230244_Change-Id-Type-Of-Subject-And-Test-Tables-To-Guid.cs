using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace student_testing_system.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIdTypeOfSubjectAndTestTablesToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NewSubjectId",
                table: "Subjects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.NewGuid());

            migrationBuilder.AddColumn<Guid>(
                name: "NewTestId",
                table: "Tests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.NewGuid());

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "SubjectId", table: "Subjects");
            migrationBuilder.DropColumn(name: "TestId", table: "Tests");

            migrationBuilder.RenameColumn(
                name: "NewSubjectId",
                table: "Subjects",
                newName: "SubjectId");

            migrationBuilder.RenameColumn(
                name: "NewTestId",
                table: "Tests",
                newName: "TestId");

        }
    }
}
