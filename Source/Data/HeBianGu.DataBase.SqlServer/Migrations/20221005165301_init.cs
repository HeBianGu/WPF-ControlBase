using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeBianGu.DataBase.SqlServer.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hi_dd_authors",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    author_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    author_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISENBLED = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hi_dd_authors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hi_dd_operations",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationType = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<bool>(type: "bit", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISENBLED = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hi_dd_operations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hi_dd_roles",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    role_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISENBLED = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hi_dd_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hl_dm_debugs",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    level = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISENBLED = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hl_dm_debugs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hl_dm_errors",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    level = table.Column<int>(type: "int", nullable: false),
                    parameter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    excetion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    stack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISENBLED = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hl_dm_errors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hl_dm_fatals",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    level = table.Column<int>(type: "int", nullable: false),
                    parameter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    excetion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    stack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    system = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    process = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISENBLED = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hl_dm_fatals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hl_dm_infos",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    level = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISENBLED = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hl_dm_infos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hl_dm_warns",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    level = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISENBLED = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hl_dm_warns", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hi_dd_authorhi_dd_role",
                columns: table => new
                {
                    AuthorsID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RolesID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hi_dd_authorhi_dd_role", x => new { x.AuthorsID, x.RolesID });
                    table.ForeignKey(
                        name: "FK_hi_dd_authorhi_dd_role_hi_dd_authors_AuthorsID",
                        column: x => x.AuthorsID,
                        principalTable: "hi_dd_authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hi_dd_authorhi_dd_role_hi_dd_roles_RolesID",
                        column: x => x.RolesID,
                        principalTable: "hi_dd_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hi_dd_users",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    account = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    display = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    role_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISENBLED = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hi_dd_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_hi_dd_users_hi_dd_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "hi_dd_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hi_dd_authorhi_dd_role_RolesID",
                table: "hi_dd_authorhi_dd_role",
                column: "RolesID");

            migrationBuilder.CreateIndex(
                name: "IX_hi_dd_users_role_id",
                table: "hi_dd_users",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hi_dd_authorhi_dd_role");

            migrationBuilder.DropTable(
                name: "hi_dd_operations");

            migrationBuilder.DropTable(
                name: "hi_dd_users");

            migrationBuilder.DropTable(
                name: "hl_dm_debugs");

            migrationBuilder.DropTable(
                name: "hl_dm_errors");

            migrationBuilder.DropTable(
                name: "hl_dm_fatals");

            migrationBuilder.DropTable(
                name: "hl_dm_infos");

            migrationBuilder.DropTable(
                name: "hl_dm_warns");

            migrationBuilder.DropTable(
                name: "hi_dd_authors");

            migrationBuilder.DropTable(
                name: "hi_dd_roles");
        }
    }
}
