using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeBianGu.DataBase.Sqlite.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hi_dd_authors",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    author_name = table.Column<string>(type: "TEXT", nullable: false),
                    author_code = table.Column<string>(type: "TEXT", nullable: true),
                    CDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ISENBLED = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hi_dd_authors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hi_dd_operations",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    UserID = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    OperationType = table.Column<int>(type: "INTEGER", nullable: false),
                    Result = table.Column<bool>(type: "INTEGER", nullable: false),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ISENBLED = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hi_dd_operations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hi_dd_roles",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    role_name = table.Column<string>(type: "TEXT", nullable: false),
                    role_code = table.Column<string>(type: "TEXT", nullable: true),
                    CDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ISENBLED = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hi_dd_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hl_dm_debugs",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    level = table.Column<int>(type: "INTEGER", nullable: false),
                    title = table.Column<string>(type: "TEXT", nullable: true),
                    value = table.Column<string>(type: "TEXT", nullable: true),
                    message = table.Column<string>(type: "TEXT", nullable: true),
                    CDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ISENBLED = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hl_dm_debugs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hl_dm_errors",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    level = table.Column<int>(type: "INTEGER", nullable: false),
                    parameter = table.Column<string>(type: "TEXT", nullable: true),
                    message = table.Column<string>(type: "TEXT", nullable: true),
                    excetion = table.Column<string>(type: "TEXT", nullable: true),
                    stack = table.Column<string>(type: "TEXT", nullable: true),
                    CDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ISENBLED = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hl_dm_errors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hl_dm_fatals",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    level = table.Column<int>(type: "INTEGER", nullable: false),
                    parameter = table.Column<string>(type: "TEXT", nullable: true),
                    message = table.Column<string>(type: "TEXT", nullable: true),
                    excetion = table.Column<string>(type: "TEXT", nullable: true),
                    stack = table.Column<string>(type: "TEXT", nullable: true),
                    system = table.Column<string>(type: "TEXT", nullable: true),
                    process = table.Column<string>(type: "TEXT", nullable: true),
                    CDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ISENBLED = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hl_dm_fatals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hl_dm_infos",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    level = table.Column<int>(type: "INTEGER", nullable: false),
                    title = table.Column<string>(type: "TEXT", nullable: true),
                    value = table.Column<string>(type: "TEXT", nullable: true),
                    message = table.Column<string>(type: "TEXT", nullable: true),
                    CDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ISENBLED = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hl_dm_infos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hl_dm_warns",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    level = table.Column<int>(type: "INTEGER", nullable: false),
                    title = table.Column<string>(type: "TEXT", nullable: true),
                    value = table.Column<string>(type: "TEXT", nullable: true),
                    message = table.Column<string>(type: "TEXT", nullable: true),
                    CDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ISENBLED = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hl_dm_warns", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hi_dd_authorhi_dd_role",
                columns: table => new
                {
                    AuthorsID = table.Column<string>(type: "TEXT", nullable: false),
                    RolesID = table.Column<string>(type: "TEXT", nullable: false)
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
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    user_name = table.Column<string>(type: "TEXT", nullable: false),
                    account = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false),
                    display = table.Column<string>(type: "TEXT", nullable: true),
                    role_id = table.Column<string>(type: "TEXT", nullable: false),
                    CDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ISENBLED = table.Column<int>(type: "INTEGER", nullable: false)
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
