using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUSAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "branch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Branchcode = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true),
                    Branchname = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true),
                    BranchAddress = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false , defaultValueSql: "('A')"),
                    isDelete = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_branch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true),
                    Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    isDelete = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "maintenance",
                columns: table => new
                {
                    Document = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true),
                    Branches = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "prmt_attachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<int>(type: "int", nullable: true),
                    Filename = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true),
                    NewFilename = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Filepath = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    IsDelete = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prmt_attachment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "prmt_details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<int>(type: "int", nullable: true),
                    Document = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true),
                    DateUploaded = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    FullDetails = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    isDelete = table.Column<byte>(type: "tinyint", nullable: true, defaultValueSql: "((0))"),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prmt_details", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    DocumentEntry = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "user_list",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 55, nullable: true),
                    ConfirmPassword = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 55, nullable: true),
                    Email = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true),
                    Lastname = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true),
                    Firstname = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true),
                    Middlename = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true),
                    ContactNumber = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: true),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 55, nullable: true),
                    Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    isDelete = table.Column<byte>(type: "tinyint", nullable: true, defaultValueSql: "((0))"),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserType = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true),
                    AccountType = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_list", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "branch");

            migrationBuilder.DropTable(
                name: "document");

            migrationBuilder.DropTable(
                name: "maintenance");

            migrationBuilder.DropTable(
                name: "prmt_attachment");

            migrationBuilder.DropTable(
                name: "prmt_details");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "user_list");


        }
    }
}
