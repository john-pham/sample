using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ebrain.Migrations
{
    public partial class Superbrain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    FAX = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    LogoName = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    IsHQ = table.Column<string>(type: "bit", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.BranchId);
                });

            migrationBuilder.CreateTable(
                name: "BranchHead",
                columns: table => new
                {
                    BranchHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchParentId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),

                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchHead", x => x.BranchHeadId);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    ClassName = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),

                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.ClassId);
                });

            migrationBuilder.CreateTable(
                name: "ClassHead",
                columns: table => new
                {
                    ClassHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                    ClassId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),

                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassHead", x => x.ClassHeadId);
                });

            migrationBuilder.CreateTable(
                name: "Consultant",
                columns: table => new
                {
                    ConsultantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsultantCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    ConsultantName = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),

                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultant", x => x.ConsultantId);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupDocumentId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                    DocumentCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    DocumentName = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(1000)", nullable: true),

                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.DocumentId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentHead",
                columns: table => new
                {
                    DocumentHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                    DocumentId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),

                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentHead", x => x.DocumentHeadId);
                });

            migrationBuilder.CreateTable(
                name: "GroupDocument",
                columns: table => new
                {
                    GroupDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupDocumentCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    GroupDocumentName = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),

                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDocument", x => x.GroupDocumentId);
                });

            migrationBuilder.CreateTable(
                name: "GroupDocumentHead",
                columns: table => new
                {
                    GrpDocumentHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                    GroupDocumentId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),

                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDocumentHead", x => x.GrpDocumentHeadId);
                });

            migrationBuilder.CreateTable(
                name: "GrpMaterial",
                columns: table => new
                {
                    GrpMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrpMaterialCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    GrpMaterialName = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),

                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrpMaterial", x => x.GrpMaterialId);
                });

            migrationBuilder.CreateTable(
                name: "GrpMaterialHead",
                columns: table => new
                {
                    GrpMaterialHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                    GrpMaterialId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),

                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrpMaterialHead", x => x.GrpMaterialHeadId);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    InventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),

                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.InventoryId);
                });

            migrationBuilder.CreateTable(
                name: "InventoryDetail",
                columns: table => new
                {
                    InventoryDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                    Materialid = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                    InputQuantity = table.Column<decimal>(type: "decimal(18,6)", nullable: false),

                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryDetail", x => x.InventoryDetailId);
                });

            migrationBuilder.CreateTable(
                name: "IOStock",
                columns: table => new
                {
                    IOStockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IONumber = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    IOTypeId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                    SupplierId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                    BranchParentId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                    PurchaseOrderCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    PurchaseOrderId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),

                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IOStock", x => x.IOStockId);
                });

            migrationBuilder.CreateTable(
                name: "IOStockDetail",
                columns: table => new
                {
                    IOStockDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IOStockId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                    MaterialId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                    MaterialCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    MaterialName = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    InputQuantity = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    VAT = table.Column<int>(type: "int", nullable: false),
                    PriceBeforeVAT = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PriceAfterVAT = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    TotalPriceBeforeVAT = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PurchaseOrderDetailId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),

                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IOStockDetail", x => x.IOStockDetailId);
                });

            migrationBuilder.CreateTable(
                name: "IOType",
                columns: table => new
                {
                    IOTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IOTypeCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    IOTypeName = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    IsInput = table.Column<bool>(type: "bit", nullable: false),
                    JoinStockMovementSummary = table.Column<bool>(type: "bit", nullable: false),
                    JoinAverages = table.Column<bool>(type: "bit", nullable: false),

                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IOType", x => x.IOTypeId);
                });

            migrationBuilder.CreateTable(
                name: "LevelClass",
                columns: table => new
                {
                    LevelClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelClassCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    LevelClassName = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),

                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelClass", x => x.LevelClassId);
                });

            migrationBuilder.CreateTable(
               name: "LevelClassHead",
               columns: table => new
               {
                   LevelClassHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                   BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                   LevelClassId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),

                   Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                   CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                   UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                   CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                   UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                   TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                   IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                   AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                   AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                   AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_LevelClassHead", x => x.LevelClassHeadId);
               });

            migrationBuilder.CreateTable(
               name: "Material",
               columns: table => new
               {
                   MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                   GrpMaterialId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                   UnitId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                   MaterialCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                   MaterialName = table.Column<string>(type: "nvarchar(1000)", nullable: true),

                   Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                   CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                   UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                   CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                   UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                   TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                   IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                   AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                   AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                   AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Material", x => x.MaterialId);
               });

            migrationBuilder.CreateTable(
                name: "MaterialHead",
                columns: table => new
                {
                    MaterialHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                    MaterialId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),

                    VAT = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PriceAfterVAT = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    WholePrice = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    WholePriceAfterVAT = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    SellPrice = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    SellPriceAfterVAT = table.Column<decimal>(type: "decimal(18,6)", nullable: false),

                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialHead", x => x.MaterialHeadId);
                });

            migrationBuilder.CreateTable(
              name: "Payment",
              columns: table => new
              {
                  PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  PaymentTypeId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                  BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                  PaymentCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                  PaymentName = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                  TotalMoney = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                  TotalMoneyReturn = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                  TotalMoneyAgain = table.Column<decimal>(type: "decimal(18,6)", nullable: false),

                  Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                  CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                  UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                  TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                  IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                  AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                  AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                  AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Payment", x => x.PaymentId);
              });

            migrationBuilder.CreateTable(
                name: "PaymentDetail",
                columns: table => new
                {
                    PaymentDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                    IOStockId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                    IONumber = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),

                    VAT = table.Column<int>(type: "int", nullable: false),
                    PriceBeforeVAT = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PriceAfterVAT = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,6)", nullable: false),

                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetail", x => x.PaymentDetailId);
                });

            migrationBuilder.CreateTable(
              name: "PaymentType",
              columns: table => new
              {
                  PaymentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                  PaymentTypeCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                  PaymentTypeName = table.Column<string>(type: "nvarchar(1000)", nullable: true),

                  Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                  CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                  UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                  TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                  IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                  AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                  AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                  AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_PaymentType", x => x.PaymentTypeId);
              });

            migrationBuilder.CreateTable(
                name: "PaymentTypeHead",
                columns: table => new
                {
                    PaymentTypeHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                    PaymentTypeId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),

                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypeHead", x => x.PaymentTypeHeadId);
                });

            migrationBuilder.CreateTable(
              name: "Promotion",
              columns: table => new
              {
                  PromotionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                  PromotionCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                  PromotionName = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                  StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                  EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                  IsActive = table.Column<bool>(type: "bit", nullable: false),

                  Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                  CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                  UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                  TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                  IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                  AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                  AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                  AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Promotion", x => x.PromotionId);
              });

            migrationBuilder.CreateTable(
             name: "PromotionDetail",
             columns: table => new
             {
                 PromotionDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 PromotionId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                 MaterialId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                 MaterialCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 MaterialName = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                 DiscountPercent = table.Column<int>(type: "int", nullable: true),
                 DiscountMoney = table.Column<decimal>(type: "decimal(18,6)", nullable: false),

                 Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                 CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                 IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                 AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                 AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                 AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_PromotionDetail", x => x.PromotionDetailId);
             });

            migrationBuilder.CreateTable(
              name: "PurchaseOrder",
              columns: table => new
              {
                  PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  BranchId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                  PurchaseOrderCode = table.Column<string>(type: "nvarchar(256)", nullable: true),

                  Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                  CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                  UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                  TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                  IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                  AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                  AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                  AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_PurchaseOrder", x => x.PurchaseOrderId);
              });

            migrationBuilder.CreateTable(
             name: "PurchaseOrderDetail",
             columns: table => new
             {
                 PurchaseOrderDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 PurchaseOrderId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                 MaterialId = table.Column<Guid?>(type: "uniqueidentifier", nullable: true),
                 MaterialCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 MaterialName = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                 InputQuantity = table.Column<decimal>(type: "decimal(18,6)", nullable: false),

                 Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                 CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                 IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                 AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                 AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                 AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_PurchaseOrderDetail", x => x.PurchaseOrderDetailId);
             });

            migrationBuilder.CreateTable(
             name: "Relationship",
             columns: table => new
             {
                 RelationshipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 RelationshipCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 RelationshipName = table.Column<string>(type: "nvarchar(1000)", nullable: true),

                 Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                 CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                 IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                 AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                 AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                 AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_Relationship", x => x.RelationshipId);
             });

            migrationBuilder.CreateTable(
             name: "Room",
             columns: table => new
             {
                 RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 RoomCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 RoomName = table.Column<string>(type: "nvarchar(1000)", nullable: true),

                 Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                 CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                 IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                 AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                 AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                 AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_Room", x => x.RoomId);
             });

            migrationBuilder.CreateTable(
             name: "RoomHead",
             columns: table => new
             {
                 RoomHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),

                 Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                 CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                 IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                 AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                 AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                 AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_RoomHead", x => x.RoomHeadId);
             });

            migrationBuilder.CreateTable(
             name: "ShiftClass",
             columns: table => new
             {
                 ShiftClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 ShiftClassCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 ShiftClassName = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                 StartTime = table.Column<DateTime>(type: "datetime", nullable: false),
                 EndTime = table.Column<DateTime>(type: "datetime", nullable: false),

                 Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                 CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                 IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                 AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                 AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                 AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_ShiftClass", x => x.ShiftClassId);
             });

            migrationBuilder.CreateTable(
             name: "ShiftClassHead",
             columns: table => new
             {
                 ShiftClassHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 ShiftClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),

                 Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                 CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                 IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                 AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                 AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                 AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_ShiftClassHead", x => x.ShiftClassHeadId);
             });

            migrationBuilder.CreateTable(
             name: "Stock",
             columns: table => new
             {
                 StockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 StockCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 StockName = table.Column<string>(type: "nvarchar(1000)", nullable: true),

                 Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                 CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                 IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                 AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                 AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                 AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_Stock", x => x.StockId);
             });

            migrationBuilder.CreateTable(
             name: "StockHead",
             columns: table => new
             {
                 StockHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 StockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),

                 Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                 CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                 IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                 AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                 AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                 AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_StockHead", x => x.StockHeadId);
             });

            migrationBuilder.CreateTable(
             name: "Student",
             columns: table => new
             {
                 StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 StudentCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 StudentName = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                 Address = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                 TaxCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 Phone = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 Email = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 AccountBank = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 Male = table.Column<bool>(type: "bit", nullable: false),
                 UserName = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 Password = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 SchoolName = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                 ClassName = table.Column<string>(type: "nvarchar(256)", nullable: true),

                 Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                 CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                 IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                 AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                 AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                 AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_Student", x => x.StudentId);
             });

            migrationBuilder.CreateTable(
             name: "StudentRelationShip",
             columns: table => new
             {
                 StudentRelationShipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 Address = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                 TaxCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 Phone = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 Email = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 AccountBank = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 RelationShipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 RelationRequire = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                 Facebook = table.Column<string>(type: "nvarchar(256)", nullable: true),

                 Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                 CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                 IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                 AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                 AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                 AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_StudentRelationShip", x => x.StudentRelationShipId);
             });

            migrationBuilder.CreateTable(
             name: "Supplier",
             columns: table => new
             {
                 SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 GrpSupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 SupplierCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 SupplierName = table.Column<string>(type: "nvarchar(1000)", nullable: true),

                 Address = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                 TaxCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 Phone = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 Email = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 AccountBank = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 RelationShipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 RelationRequire = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                 Facebook = table.Column<string>(type: "nvarchar(256)", nullable: true),

                 Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                 CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                 IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                 AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                 AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                 AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_Supplier", x => x.SupplierId);
             });

            migrationBuilder.CreateTable(
             name: "Unit",
             columns: table => new
             {
                 UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 UnitCode = table.Column<string>(type: "nvarchar(256)", nullable: true),
                 UnitName = table.Column<string>(type: "nvarchar(1000)", nullable: true),

                 Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                 CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                 TimeStamp = table.Column<byte[]>(type: "timestamp", unicode: false, maxLength: 30, nullable: false),
                 IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                 AutoLogOnCode_LastUpdate_IP = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                 AutoLogOnCode_LastUpdate_ComputerName = table.Column<DateTime>(type: "datetime", nullable: true),
                 AutoLogOnCode_LastUpdate_MACAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_Unit", x => x.UnitId);
             });


            migrationBuilder.CreateIndex(name: "IX_Branch_BranchName", table: "Branch", column: "BranchName");
            migrationBuilder.CreateIndex(name: "IX_Class_ClassCode", table: "Class", column: "ClassCode");
            migrationBuilder.CreateIndex(name: "IX_Consultant_ConsultantName", table: "Consultant", column: "ConsultantName");
            migrationBuilder.CreateIndex(name: "IX_Document_DocumentName", table: "Document", column: "DocumentName");
            migrationBuilder.CreateIndex(name: "IX_Document_GroupDocumentName", table: "GroupDocument", column: "GroupDocumentName");
            migrationBuilder.CreateIndex(name: "IX_GrpMaterial_GrpMaterialCode", table: "GrpMaterial", column: "GrpMaterialCode");
            migrationBuilder.CreateIndex(name: "IX_LevelClass_LevelClassCode", table: "LevelClass", column: "LevelClassCode");
            migrationBuilder.CreateIndex(name: "IX_Material_MaterialCode", table: "Material", column: "MaterialCode");
            migrationBuilder.CreateIndex(name: "IX_Relationship_RelationshipCode", table: "Relationship", column: "RelationshipCode");
            migrationBuilder.CreateIndex(name: "IX_Room_RoomCode", table: "Room", column: "RoomCode");
            migrationBuilder.CreateIndex(name: "IX_Unit_UnitCode", table: "Unit", column: "UnitCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "BranchHead");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "ClassHead");

            migrationBuilder.DropTable(
                name: "Consultant");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "DocumentHead");

            migrationBuilder.DropTable(
                name: "GroupDocument");

            migrationBuilder.DropTable(
                name: "GroupDocumentHead");

            migrationBuilder.DropTable(
                name: "GrpMaterial");

            migrationBuilder.DropTable(
                name: "GrpMaterialHead");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "InventoryDetail");

            migrationBuilder.DropTable(
                name: "IOStock");

            migrationBuilder.DropTable(
                name: "IOStockDetail");

            migrationBuilder.DropTable(
                name: "IOType");

            migrationBuilder.DropTable(
                name: "LevelClass");

            migrationBuilder.DropTable(
                name: "LevelClassHead");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "MaterialHead");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "PaymentDetail");

            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.DropTable(
                name: "PaymentTypeHead");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropTable(
                name: "PromotionDetail");

            migrationBuilder.DropTable(
                name: "PurchaseOrder");

            migrationBuilder.DropTable(
                name: "PurchaseOrderDetail");

            migrationBuilder.DropTable(
                name: "Relationship");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "RoomHead");

            migrationBuilder.DropTable(
                name: "ShiftClass");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "StockHead");

            migrationBuilder.DropTable(
                 name: "Student");

            migrationBuilder.DropTable(
                name: "StudentRelationShip");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Unit");

        }
    }
}
