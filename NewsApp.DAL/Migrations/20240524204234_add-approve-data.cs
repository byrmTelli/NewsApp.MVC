using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addapprovedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostApproveRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    PostId = table.Column<string>(type: "TEXT", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostApproveRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserApproveRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ApprovedUserId = table.Column<string>(type: "TEXT", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserApproveRecords", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostApproveRecords");

            migrationBuilder.DropTable(
                name: "UserApproveRecords");
        }
    }
}
