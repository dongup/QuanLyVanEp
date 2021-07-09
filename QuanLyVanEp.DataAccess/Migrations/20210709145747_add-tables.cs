using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyVanEp.DataAccess.Migrations
{
    public partial class addtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputNumber = table.Column<float>(type: "real", nullable: false),
                    StockNumber = table.Column<float>(type: "real", nullable: false),
                    MaterialGroup = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialResponse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputNumber = table.Column<float>(type: "real", nullable: false),
                    StockNumber = table.Column<float>(type: "real", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    HeSo = table.Column<float>(type: "real", nullable: false),
                    ExpireDateWarning = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desciption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inputs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    No = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    MaterialId = table.Column<int>(type: "int", nullable: true),
                    Lotno = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    InputNumber = table.Column<float>(type: "real", nullable: false),
                    StockNumber = table.Column<float>(type: "real", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Supplier = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InputDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inputs_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LotNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SLDatHang = table.Column<int>(type: "int", nullable: false),
                    SLXuatKho = table.Column<int>(type: "int", nullable: false),
                    SLSX = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lots_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutputReponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InputId = table.Column<int>(type: "int", nullable: true),
                    Lotno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialId = table.Column<int>(type: "int", nullable: true),
                    OutputNumber = table.Column<float>(type: "real", nullable: false),
                    OriginNumber = table.Column<float>(type: "real", nullable: false),
                    AfterNumber = table.Column<float>(type: "real", nullable: false),
                    OutputDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MachineId = table.Column<int>(type: "int", nullable: true),
                    LotResponseId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutputReponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutputReponse_Lots_LotResponseId",
                        column: x => x.LotResponseId,
                        principalTable: "Lots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutputReponse_MaterialResponse_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "MaterialResponse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Outputs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LotId = table.Column<int>(type: "int", nullable: false),
                    LotsId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    InputId = table.Column<int>(type: "int", nullable: true),
                    Lotno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialId = table.Column<int>(type: "int", nullable: true),
                    OutputNumber = table.Column<float>(type: "real", nullable: false),
                    OriginNumber = table.Column<float>(type: "real", nullable: false),
                    AfterNumber = table.Column<float>(type: "real", nullable: false),
                    Purpose = table.Column<int>(type: "int", nullable: false),
                    WastedNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OutputDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Outputs_Inputs_InputId",
                        column: x => x.InputId,
                        principalTable: "Inputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Outputs_Lots_LotsId",
                        column: x => x.LotsId,
                        principalTable: "Lots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Outputs_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "261d9c77-1d6d-4a3d-a609-6e1d18ac404b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "157c6394-790e-4475-bb12-3265d97a4776");

            migrationBuilder.CreateIndex(
                name: "IX_Inputs_MaterialId",
                table: "Inputs",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_ProductId",
                table: "Lots",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OutputReponse_LotResponseId",
                table: "OutputReponse",
                column: "LotResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_OutputReponse_MaterialId",
                table: "OutputReponse",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Outputs_InputId",
                table: "Outputs",
                column: "InputId");

            migrationBuilder.CreateIndex(
                name: "IX_Outputs_LotsId",
                table: "Outputs",
                column: "LotsId");

            migrationBuilder.CreateIndex(
                name: "IX_Outputs_MaterialId",
                table: "Outputs",
                column: "MaterialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutputReponse");

            migrationBuilder.DropTable(
                name: "Outputs");

            migrationBuilder.DropTable(
                name: "MaterialResponse");

            migrationBuilder.DropTable(
                name: "Inputs");

            migrationBuilder.DropTable(
                name: "Lots");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f387444c-dcd9-4d97-9247-6a5ca07f7a55");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "cccce04e-401f-482c-9ad5-5db30ae1cdaf");
        }
    }
}
