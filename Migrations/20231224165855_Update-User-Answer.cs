using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace student_testing_system.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssignedQuestionId",
                table: "UserAnswers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_AssignedQuestionId",
                table: "UserAnswers",
                column: "AssignedQuestionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_AssignedQuestions_AssignedQuestionId",
                table: "UserAnswers",
                column: "AssignedQuestionId",
                principalTable: "AssignedQuestions",
                principalColumn: "AssignedQuestionId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_AssignedQuestions_AssignedQuestionId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_AssignedQuestionId",
                table: "UserAnswers");

            migrationBuilder.DropColumn(
                name: "AssignedQuestionId",
                table: "UserAnswers");
        }
    }
}
