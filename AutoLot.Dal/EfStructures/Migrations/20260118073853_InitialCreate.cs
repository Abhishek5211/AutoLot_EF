using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoLot.Dal.EfStructures.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.EnsureSchema(
                name: "Logging");

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, computedColumnSql: "[LastName] + ', ' + [FirstName]"),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, computedColumnSql: "[LastName] + ', ' + [FirstName]"),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Makes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makes", x => x.Id);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "MakesAudit")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidTo")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidFrom");

            migrationBuilder.CreateTable(
                name: "SeriLogs",
                schema: "Logging",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MessageTemplate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    Exception = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Properties = table.Column<string>(type: "Xml", maxLength: 50, nullable: true),
                    LogEvent = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SourceContext = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RequestPath = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApplicationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MachineName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MemberName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LineNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditRisks",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditRisks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditRisks_Customers",
                        column: x => x.CustomerID,
                        principalSchema: "dbo",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MakeId = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", maxLength: 50, nullable: true),
                    NickName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Display = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, computedColumnSql: "[NickName] + '(' + [Color] + ')'", stored: true),
                    DateBuilt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    IsDrivable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventory_Makes_MakeId",
                        column: x => x.MakeId,
                        principalSchema: "dbo",
                        principalTable: "Makes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "InventoryAudit")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidTo")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidFrom");

            migrationBuilder.CreateTable(
                name: "CarsToDrivers",
                schema: "dbo",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodEndColumn", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarsToDrivers", x => new { x.InventoryId, x.DriverId });
                    table.ForeignKey(
                        name: "FK_InventoryDriver_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalSchema: "dbo",
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryDrivers_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalSchema: "dbo",
                        principalTable: "Inventory",
                        principalColumn: "Id");
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "InventoryToDriversAudit")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidTo")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidFrom");

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Inventory",
                        column: x => x.InventoryId,
                        principalSchema: "dbo",
                        principalTable: "Inventory",
                        principalColumn: "Id");
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "OrdersAudit")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidTo")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidFrom");

            migrationBuilder.CreateTable(
                name: "Radios",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HasTweeters = table.Column<bool>(type: "bit", nullable: false),
                    HasSubWoofers = table.Column<bool>(type: "bit", nullable: false),
                    RadioId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Radios_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalSchema: "dbo",
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "RadiosAudit")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidTo")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidFrom");

            migrationBuilder.CreateIndex(
                name: "IX_CarsToDrivers_DriverId",
                schema: "dbo",
                table: "CarsToDrivers",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditRisks_CustomerID",
                schema: "dbo",
                table: "CreditRisks",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_MakeId",
                schema: "dbo",
                table: "Inventory",
                column: "MakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                schema: "dbo",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_InventoryId",
                schema: "dbo",
                table: "Orders",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Radios_CarId",
                schema: "dbo",
                table: "Radios",
                column: "InventoryId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarsToDrivers",
                schema: "dbo")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "InventoryToDriversAudit")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidTo")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidFrom");

            migrationBuilder.DropTable(
                name: "CreditRisks",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "dbo")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "OrdersAudit")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidTo")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidFrom");

            migrationBuilder.DropTable(
                name: "Radios",
                schema: "dbo")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "RadiosAudit")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidTo")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidFrom");

            migrationBuilder.DropTable(
                name: "SeriLogs",
                schema: "Logging");

            migrationBuilder.DropTable(
                name: "Drivers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Inventory",
                schema: "dbo")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "InventoryAudit")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidTo")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidFrom");

            migrationBuilder.DropTable(
                name: "Makes",
                schema: "dbo")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "MakesAudit")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidTo")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidFrom");
        }
    }
}
