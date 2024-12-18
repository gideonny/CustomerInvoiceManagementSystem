using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomerInvoiceManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceOrState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipOrPostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTerms",
                columns: table => new
                {
                    PaymentTermsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTerms", x => x.PaymentTermsId);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentTotal = table.Column<double>(type: "float", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentTermsId = table.Column<int>(type: "int", nullable: false),
                    PaymentTermsId1 = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CustomerId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Customer_CustomerId1",
                        column: x => x.CustomerId1,
                        principalTable: "Customer",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_Invoices_PaymentTerms_PaymentTermsId",
                        column: x => x.PaymentTermsId,
                        principalTable: "PaymentTerms",
                        principalColumn: "PaymentTermsId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_PaymentTerms_PaymentTermsId1",
                        column: x => x.PaymentTermsId1,
                        principalTable: "PaymentTerms",
                        principalColumn: "PaymentTermsId");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLineItems",
                columns: table => new
                {
                    InvoiceLineItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceId = table.Column<int>(type: "int", nullable: true),
                    InvoiceId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLineItems", x => x.InvoiceLineItemId);
                    table.ForeignKey(
                        name: "FK_InvoiceLineItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceLineItems_Invoices_InvoiceId1",
                        column: x => x.InvoiceId1,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId");
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "Address1", "Address2", "City", "ContactEmail", "ContactFirstName", "ContactLastName", "IsDeleted", "Name", "Phone", "ProvinceOrState", "ZipOrPostalCode" },
                values: new object[,]
                {
                    { 1, "27371 Valderas", null, "Mission Viejo", "kgonz@bja.com", "Keeton", "Gonzalo", false, "Blanchard & Johnson Associates", "214-555-3647", "CA", "92691" },
                    { 2, "3255 Ramos Cir", null, "Sacramento", "manton@gmail.com", "Mauro", "Anton", false, "California Chamber Of Commerce", "916-555-6670", "CA", "95827" },
                    { 3, "PO Box 85826", null, "San Diego", null, "Jane", "Smith", false, "Golden Eagle Insurance Co", "917-544-7090", "CA", "92186" },
                    { 4, "1952 H Street", "P.O. Box 1952", "Fresno", null, "Chad", "Jones", false, "Fresno Photoengraving Company", "559-555-3005", "CA", "93718" },
                    { 5, "Ohio Valley Litho Division", null, "Cincinnate", null, "Paul", "Morgan", false, "Nielson", "519-824-3477", "OH", "45264" },
                    { 6, "PO Box 40513", null, "Jacksonville", "gerald.kristoff@outlook.com", "Gerald", "Kristoff", false, "Naylor Publications Inc", "800-555-6041", "FL", "32231" },
                    { 7, "60 Madison Ave", null, "New York", "tneftaly@venture.com", "Thalia", "Neftaly", false, "Venture Communications", "212-533-4800", "NY", "10010" },
                    { 8, "Attn:  Supt. Window Services", "PO Box 7005", "Madison", null, "Alberto", "Francesco", false, "US Postal Service", "800-555-1205", "WI", "53707" }
                });

            migrationBuilder.InsertData(
                table: "PaymentTerms",
                columns: new[] { "PaymentTermsId", "Description", "DueDays" },
                values: new object[,]
                {
                    { 1, "Net due 10 days", 10 },
                    { 2, "Net due 20 days", 20 },
                    { 3, "Net due 30 days", 30 },
                    { 4, "Net due 60 days", 60 },
                    { 5, "Net due 90 days", 90 }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "CustomerId", "CustomerId1", "InvoiceDate", "PaymentDate", "PaymentTermsId", "PaymentTermsId1", "PaymentTotal" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2022, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, null, 0.0 },
                    { 2, 1, null, new DateTime(2022, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, null, 0.0 },
                    { 3, 2, null, new DateTime(2022, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, null, 0.0 },
                    { 4, 2, null, new DateTime(2022, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, null, 0.0 },
                    { 5, 3, null, new DateTime(2022, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, null, 0.0 },
                    { 6, 3, null, new DateTime(2022, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, null, 0.0 },
                    { 7, 4, null, new DateTime(2022, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, null, 0.0 },
                    { 8, 4, null, new DateTime(2022, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, null, 0.0 },
                    { 9, 5, null, new DateTime(2022, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, null, 0.0 },
                    { 10, 6, null, new DateTime(2022, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, null, 0.0 },
                    { 11, 7, null, new DateTime(2022, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, null, 0.0 },
                    { 12, 8, null, new DateTime(2022, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, null, 0.0 }
                });

            migrationBuilder.InsertData(
                table: "InvoiceLineItems",
                columns: new[] { "InvoiceLineItemId", "Amount", "Description", "InvoiceId", "InvoiceId1" },
                values: new object[,]
                {
                    { 1, 1089.99, "Part 123", 1, null },
                    { 2, 173499.5, "Service XYZ", 1, null },
                    { 3, 4654499.5, "Part 110", 2, null },
                    { 4, 78799.5, "Part A", 2, null },
                    { 5, 679.5, "Part AA", 3, null },
                    { 6, 786.89999999999998, "Part Z", 3, null },
                    { 7, 998.5, "Trip 1", 4, null },
                    { 8, 1011.5, "Service XYZ", 4, null },
                    { 9, 565735.5, "Rental DEF", 5, null },
                    { 10, 5753.5, "Rental ZZZ", 5, null },
                    { 11, 58453.900000000001, "Service ABC", 6, null },
                    { 12, 111232.5, "Service ABC", 6, null },
                    { 13, 109.5, "Rental ABC", 7, null },
                    { 14, 57846.5, "Rental ABC", 8, null },
                    { 15, 132.5, "Trip 2", 9, null },
                    { 16, 6877.8999999999996, "Service XYZ", 9, null },
                    { 17, 8999.5, "Trip 3", 10, null },
                    { 18, 1033.5, "Blah blah", 10, null },
                    { 19, 56455.5, "Ho hum", 11, null },
                    { 20, 1454589.5, "Fiddle dee", 11, null },
                    { 21, 583453.5, "Service ABC", 12, null },
                    { 22, 567.5, "Fiddle dum", 12, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineItems_InvoiceId",
                table: "InvoiceLineItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineItems_InvoiceId1",
                table: "InvoiceLineItems",
                column: "InvoiceId1");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId1",
                table: "Invoices",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentTermsId",
                table: "Invoices",
                column: "PaymentTermsId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentTermsId1",
                table: "Invoices",
                column: "PaymentTermsId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceLineItems");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "PaymentTerms");
        }
    }
}
