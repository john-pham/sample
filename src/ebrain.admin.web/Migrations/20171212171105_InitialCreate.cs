using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ebrain.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "OpenIddictTokens");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "OpenIddictTokens");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "OpenIddictTokens");

            migrationBuilder.DropColumn(
                name: "Scopes",
                table: "OpenIddictAuthorizations");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "OpenIddictAuthorizations");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "OpenIddictAuthorizations",
                newName: "Scope");

            migrationBuilder.CreateTable(
                name: "BranchHeads",
                columns: table => new
                {
                    BranchHeadId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    BranchParentId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchHeads", x => x.BranchHeadId);
                });

            migrationBuilder.CreateTable(
                name: "Branchs",
                columns: table => new
                {
                    BranchId = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchCode = table.Column<string>(maxLength: 256, nullable: true),
                    BranchName = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    FAX = table.Column<string>(maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsHQ = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branchs", x => x.BranchId);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    ClassId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    ClassCode = table.Column<string>(maxLength: 256, nullable: true),
                    ClassName = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.ClassId);
                });

            migrationBuilder.CreateTable(
                name: "ClassHeads",
                columns: table => new
                {
                    ClassHeadId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    ClassId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassHeads", x => x.ClassHeadId);
                });

            migrationBuilder.CreateTable(
                name: "Consultants",
                columns: table => new
                {
                    ConsultantId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    ConsultantCode = table.Column<string>(nullable: true),
                    ConsultantName = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultants", x => x.ConsultantId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentHeads",
                columns: table => new
                {
                    DocumentHeadId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DocumentId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentHeads", x => x.DocumentHeadId);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DocumentCode = table.Column<string>(nullable: true),
                    DocumentName = table.Column<string>(nullable: true),
                    GroupDocumentId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentId);
                });

            migrationBuilder.CreateTable(
                name: "GroupDocumentHeads",
                columns: table => new
                {
                    GrpDocumentHeadId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    GroupDocumentId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDocumentHeads", x => x.GrpDocumentHeadId);
                });

            migrationBuilder.CreateTable(
                name: "GroupDocuments",
                columns: table => new
                {
                    GroupDocumentId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    GroupDocumentCode = table.Column<string>(nullable: true),
                    GroupDocumentName = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDocuments", x => x.GroupDocumentId);
                });

            migrationBuilder.CreateTable(
                name: "GrpMaterials",
                columns: table => new
                {
                    GrpMaterialId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    GrpMaterialCode = table.Column<string>(nullable: true),
                    GrpMaterialName = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrpMaterials", x => x.GrpMaterialId);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    InventoryId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.InventoryId);
                });

            migrationBuilder.CreateTable(
                name: "InventoryDetails",
                columns: table => new
                {
                    InventoryDetailId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    InputQuantity = table.Column<decimal>(nullable: true),
                    InventoryId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Materialid = table.Column<Guid>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryDetails", x => x.InventoryDetailId);
                });

            migrationBuilder.CreateTable(
                name: "IOStockDetails",
                columns: table => new
                {
                    IOStockDetailId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IOStockId = table.Column<Guid>(nullable: true),
                    InputQuantity = table.Column<decimal>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MaterialCode = table.Column<string>(nullable: true),
                    MaterialId = table.Column<Guid>(nullable: true),
                    MaterialName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    PriceAfterVAT = table.Column<decimal>(nullable: true),
                    PriceBeforeVAT = table.Column<decimal>(nullable: true),
                    PurchaseOrderDetailId = table.Column<Guid>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    TotalPrice = table.Column<decimal>(nullable: true),
                    TotalPriceBeforeVAT = table.Column<decimal>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    VAT = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IOStockDetails", x => x.IOStockDetailId);
                });

            migrationBuilder.CreateTable(
                name: "IOStocks",
                columns: table => new
                {
                    IOStockId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    BranchParentId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IONumber = table.Column<string>(nullable: true),
                    IOTypeId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    PurchaseOrderCode = table.Column<string>(nullable: true),
                    PurchaseOrderId = table.Column<Guid>(nullable: true),
                    SupplierId = table.Column<Guid>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IOStocks", x => x.IOStockId);
                });

            migrationBuilder.CreateTable(
                name: "IOTypes",
                columns: table => new
                {
                    IOTypeId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IOTypeCode = table.Column<string>(nullable: true),
                    IOTypeName = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsInput = table.Column<bool>(nullable: false),
                    JoinAverages = table.Column<bool>(nullable: false),
                    JoinStockMovementSummary = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IOTypes", x => x.IOTypeId);
                });

            migrationBuilder.CreateTable(
                name: "LevelClass",
                columns: table => new
                {
                    LevelClassId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LevelClassCode = table.Column<string>(nullable: true),
                    LevelClassName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelClass", x => x.LevelClassId);
                });

            migrationBuilder.CreateTable(
                name: "LevelClassHeads",
                columns: table => new
                {
                    LevelClassHeadId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LevelClassId = table.Column<Guid>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelClassHeads", x => x.LevelClassHeadId);
                });

            migrationBuilder.CreateTable(
                name: "MaterialHeads",
                columns: table => new
                {
                    MaterialHeadId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MaterialId = table.Column<Guid>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: true),
                    PriceAfterVAT = table.Column<decimal>(nullable: true),
                    SellPrice = table.Column<decimal>(nullable: true),
                    SellPriceAfterVAT = table.Column<decimal>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    VAT = table.Column<int>(nullable: true),
                    WholePrice = table.Column<decimal>(nullable: true),
                    WholePriceAfterVAT = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialHeads", x => x.MaterialHeadId);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    MaterialId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    GrpMaterialId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MaterialCode = table.Column<string>(nullable: true),
                    MaterialName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UnitId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.MaterialId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentDetails",
                columns: table => new
                {
                    PaymentDetailId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IONumber = table.Column<string>(nullable: true),
                    IOStockId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    PaymentId = table.Column<Guid>(nullable: true),
                    PriceAfterVAT = table.Column<decimal>(nullable: true),
                    PriceBeforeVAT = table.Column<decimal>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    TotalPrice = table.Column<decimal>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    VAT = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetails", x => x.PaymentDetailId);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    PaymentCode = table.Column<string>(nullable: true),
                    PaymentName = table.Column<string>(nullable: true),
                    PaymentTypeId = table.Column<Guid>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    TotalMoney = table.Column<decimal>(nullable: true),
                    TotalMoneyAgain = table.Column<decimal>(nullable: true),
                    TotalMoneyReturn = table.Column<decimal>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypeHeads",
                columns: table => new
                {
                    PaymentTypeHeadId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    PaymentTypeId = table.Column<Guid>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypeHeads", x => x.PaymentTypeHeadId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    PaymentTypeId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    PaymentTypeCode = table.Column<string>(nullable: true),
                    PaymentTypeName = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.PaymentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PromotionDetails",
                columns: table => new
                {
                    PromotionDetailId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DiscountMoney = table.Column<decimal>(nullable: true),
                    DiscountPercent = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MaterialCode = table.Column<string>(nullable: true),
                    MaterialId = table.Column<Guid>(nullable: true),
                    MaterialName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    PromotionId = table.Column<Guid>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDetails", x => x.PromotionDetailId);
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    PromotionId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    PromotionCode = table.Column<string>(nullable: true),
                    PromotionName = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.PromotionId);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderDetails",
                columns: table => new
                {
                    PurchaseOrderDetailId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    InputQuantity = table.Column<decimal>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MaterialCode = table.Column<string>(nullable: true),
                    MaterialId = table.Column<string>(nullable: true),
                    MaterialName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    PurchaseOrderId = table.Column<Guid>(nullable: false),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderDetails", x => x.PurchaseOrderDetailId);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    PurchaseOrderId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    PurchaseOrderCode = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.PurchaseOrderId);
                });

            migrationBuilder.CreateTable(
                name: "Relationships",
                columns: table => new
                {
                    RelationshipId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    RelationshipCode = table.Column<string>(nullable: true),
                    RelationshipName = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relationships", x => x.RelationshipId);
                });

            migrationBuilder.CreateTable(
                name: "RoomHeads",
                columns: table => new
                {
                    RoomHeadId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    RoomId = table.Column<Guid>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomHeads", x => x.RoomHeadId);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    RoomCode = table.Column<string>(nullable: true),
                    RoomName = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "ShiftClass",
                columns: table => new
                {
                    ShiftClassId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    ShiftClassCode = table.Column<string>(nullable: true),
                    ShiftClassName = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftClass", x => x.ShiftClassId);
                });

            migrationBuilder.CreateTable(
                name: "ShiftClassHeads",
                columns: table => new
                {
                    ShiftClassHeadId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    ShiftClassId = table.Column<Guid>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftClassHeads", x => x.ShiftClassHeadId);
                });

            migrationBuilder.CreateTable(
                name: "StockHeads",
                columns: table => new
                {
                    StockHeadId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    StockId = table.Column<Guid>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockHeads", x => x.StockHeadId);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    StockId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    StockCode = table.Column<string>(nullable: true),
                    StockName = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.StockId);
                });

            migrationBuilder.CreateTable(
                name: "StudentRelationShips",
                columns: table => new
                {
                    StudentRelationShipId = table.Column<Guid>(nullable: false),
                    AccountBank = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Facebook = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    RelationRequire = table.Column<string>(nullable: true),
                    RelationShipId = table.Column<Guid>(nullable: true),
                    StudentId = table.Column<Guid>(nullable: true),
                    TaxCode = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentRelationShips", x => x.StudentRelationShipId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(nullable: false),
                    AccountBank = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    ClassName = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Male = table.Column<bool>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    SchoolName = table.Column<string>(nullable: true),
                    StudentCode = table.Column<string>(nullable: true),
                    StudentName = table.Column<string>(nullable: true),
                    TaxCode = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<Guid>(nullable: false),
                    AccountBank = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    GrpSupplierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    SupplierCode = table.Column<string>(nullable: true),
                    SupplierName = table.Column<string>(nullable: true),
                    TaxCode = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    UnitId = table.Column<Guid>(nullable: false),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    UnitCode = table.Column<string>(nullable: true),
                    UnitName = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.UnitId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchHeads");

            migrationBuilder.DropTable(
                name: "Branchs");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "ClassHeads");

            migrationBuilder.DropTable(
                name: "Consultants");

            migrationBuilder.DropTable(
                name: "DocumentHeads");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "GroupDocumentHeads");

            migrationBuilder.DropTable(
                name: "GroupDocuments");

            migrationBuilder.DropTable(
                name: "GrpMaterials");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "InventoryDetails");

            migrationBuilder.DropTable(
                name: "IOStockDetails");

            migrationBuilder.DropTable(
                name: "IOStocks");

            migrationBuilder.DropTable(
                name: "IOTypes");

            migrationBuilder.DropTable(
                name: "LevelClass");

            migrationBuilder.DropTable(
                name: "LevelClassHeads");

            migrationBuilder.DropTable(
                name: "MaterialHeads");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "PaymentDetails");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PaymentTypeHeads");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "PromotionDetails");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "PurchaseOrderDetails");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Relationships");

            migrationBuilder.DropTable(
                name: "RoomHeads");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "ShiftClass");

            migrationBuilder.DropTable(
                name: "ShiftClassHeads");

            migrationBuilder.DropTable(
                name: "StockHeads");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "StudentRelationShips");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.RenameColumn(
                name: "Scope",
                table: "OpenIddictAuthorizations",
                newName: "Type");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "OpenIddictTokens",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "OpenIddictTokens",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "OpenIddictTokens",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Scopes",
                table: "OpenIddictAuthorizations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "OpenIddictAuthorizations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
