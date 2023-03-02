using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHelper.Migrations
{
    public partial class schedulle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeyHistories");

            migrationBuilder.DropTable(
                name: "PaymentItems");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "TgUsers");

            migrationBuilder.DropTable(
                name: "Keys");

            migrationBuilder.DropTable(
                name: "Amount");

            migrationBuilder.DropTable(
                name: "AuthorizationDetails");

            migrationBuilder.DropTable(
                name: "CancellationDetails");

            migrationBuilder.DropTable(
                name: "IncomeAmount");

            migrationBuilder.DropTable(
                name: "Metadata");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "Recipient");

            migrationBuilder.DropTable(
                name: "RefundedAmount");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordSalt",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "Users",
                type: "bit",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Ts",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "TokenSalt",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "TokenHash",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "RefreshTokens",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "Schedulles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchedulleJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedulles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Middlename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartWork = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SummaryPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpHistoryPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DegreePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Confirm = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SchedulleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Schedulles_SchedulleId",
                        column: x => x.SchedulleId,
                        principalTable: "Schedulles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorSpecialty",
                columns: table => new
                {
                    DoctorsId = table.Column<int>(type: "int", nullable: false),
                    SpecialtiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSpecialty", x => new { x.DoctorsId, x.SpecialtiesId });
                    table.ForeignKey(
                        name: "FK_DoctorSpecialty_Doctors_DoctorsId",
                        column: x => x.DoctorsId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorSpecialty_Specialties_SpecialtiesId",
                        column: x => x.SpecialtiesId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Active", "Login" },
                values: new object[] { true, "admin@admin.admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SchedulleId",
                table: "Doctors",
                column: "SchedulleId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSpecialty_SpecialtiesId",
                table: "DoctorSpecialty",
                column: "SpecialtiesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorSpecialty");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropTable(
                name: "Schedulles");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordSalt",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<byte>(
                name: "Active",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Ts",
                table: "RefreshTokens",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "TokenSalt",
                table: "RefreshTokens",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TokenHash",
                table: "RefreshTokens",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "RefreshTokens",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "RefreshTokens",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "Amount",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    currency = table.Column<string>(type: "longtext", nullable: true),
                    value = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amount", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorizationDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    auth_code = table.Column<string>(type: "longtext", nullable: true),
                    rrn = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizationDetails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CancellationDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    party = table.Column<string>(type: "longtext", nullable: true),
                    reason = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancellationDetails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    card_type = table.Column<string>(type: "longtext", nullable: true),
                    expiry_month = table.Column<string>(type: "longtext", nullable: true),
                    expiry_year = table.Column<string>(type: "longtext", nullable: true),
                    first6 = table.Column<string>(type: "longtext", nullable: true),
                    issuer_country = table.Column<string>(type: "longtext", nullable: true),
                    last4 = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CrmId = table.Column<string>(type: "longtext", nullable: false),
                    EmailMd5Hash = table.Column<string>(type: "longtext", nullable: false),
                    IsDeleted = table.Column<byte>(type: "tinyint(1)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomeAmount",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    currency = table.Column<string>(type: "longtext", nullable: true),
                    value = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeAmount", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Metadata",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    cms_name = table.Column<string>(type: "longtext", nullable: true),
                    cps_email = table.Column<string>(type: "longtext", nullable: true),
                    cps_phone = table.Column<string>(type: "longtext", nullable: true),
                    custName = table.Column<string>(type: "longtext", nullable: true),
                    module_version = table.Column<string>(type: "longtext", nullable: true),
                    orderDetails = table.Column<string>(type: "longtext", nullable: true),
                    wp_user_id = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metadata", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Recipient",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    account_id = table.Column<string>(type: "longtext", nullable: true),
                    gateway_id = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipient", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RefundedAmount",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    currency = table.Column<string>(type: "longtext", nullable: true),
                    value = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundedAmount", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "longtext", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TgUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ChatId = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TgUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    cardid = table.Column<int>(type: "int", nullable: true),
                    saved = table.Column<byte>(type: "tinyint(1)", nullable: true),
                    title = table.Column<string>(type: "longtext", nullable: true),
                    type = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.id);
                    table.ForeignKey(
                        name: "FK_PaymentMethod_Card_cardid",
                        column: x => x.cardid,
                        principalTable: "Card",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Keys",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    Expired = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Keys_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentItems",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    amountid = table.Column<int>(type: "int", nullable: true),
                    authorization_detailsid = table.Column<int>(type: "int", nullable: true),
                    cancellation_detailsid = table.Column<int>(type: "int", nullable: true),
                    income_amountid = table.Column<int>(type: "int", nullable: true),
                    metadataid = table.Column<int>(type: "int", nullable: true),
                    payment_methodid = table.Column<string>(type: "varchar(255)", nullable: true),
                    recipientid = table.Column<int>(type: "int", nullable: true),
                    refunded_amountid = table.Column<int>(type: "int", nullable: true),
                    captured_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    description = table.Column<string>(type: "longtext", nullable: true),
                    emailSend = table.Column<byte>(type: "tinyint(1)", nullable: false),
                    paid = table.Column<byte>(type: "tinyint(1)", nullable: true),
                    refundable = table.Column<byte>(type: "tinyint(1)", nullable: true),
                    status = table.Column<string>(type: "longtext", nullable: true),
                    test = table.Column<byte>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentItems", x => x.id);
                    table.ForeignKey(
                        name: "FK_PaymentItems_Amount_amountid",
                        column: x => x.amountid,
                        principalTable: "Amount",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_PaymentItems_AuthorizationDetails_authorization_detailsid",
                        column: x => x.authorization_detailsid,
                        principalTable: "AuthorizationDetails",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_PaymentItems_CancellationDetails_cancellation_detailsid",
                        column: x => x.cancellation_detailsid,
                        principalTable: "CancellationDetails",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_PaymentItems_IncomeAmount_income_amountid",
                        column: x => x.income_amountid,
                        principalTable: "IncomeAmount",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_PaymentItems_Metadata_metadataid",
                        column: x => x.metadataid,
                        principalTable: "Metadata",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_PaymentItems_PaymentMethod_payment_methodid",
                        column: x => x.payment_methodid,
                        principalTable: "PaymentMethod",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_PaymentItems_Recipient_recipientid",
                        column: x => x.recipientid,
                        principalTable: "Recipient",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_PaymentItems_RefundedAmount_refunded_amountid",
                        column: x => x.refunded_amountid,
                        principalTable: "RefundedAmount",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "KeyHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    KeyId = table.Column<string>(type: "varchar(255)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Changed = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Comment = table.Column<string>(type: "longtext", nullable: false),
                    CrmTaskId = table.Column<string>(type: "longtext", nullable: false),
                    Expired = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyHistories_Keys_KeyId",
                        column: x => x.KeyId,
                        principalTable: "Keys",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KeyHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Active", "Login" },
                values: new object[] { (byte)1, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_KeyHistories_KeyId",
                table: "KeyHistories",
                column: "KeyId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyHistories_UserId",
                table: "KeyHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Keys_ClientId",
                table: "Keys",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentItems_amountid",
                table: "PaymentItems",
                column: "amountid");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentItems_authorization_detailsid",
                table: "PaymentItems",
                column: "authorization_detailsid");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentItems_cancellation_detailsid",
                table: "PaymentItems",
                column: "cancellation_detailsid");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentItems_income_amountid",
                table: "PaymentItems",
                column: "income_amountid");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentItems_metadataid",
                table: "PaymentItems",
                column: "metadataid");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentItems_payment_methodid",
                table: "PaymentItems",
                column: "payment_methodid");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentItems_recipientid",
                table: "PaymentItems",
                column: "recipientid");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentItems_refunded_amountid",
                table: "PaymentItems",
                column: "refunded_amountid");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_cardid",
                table: "PaymentMethod",
                column: "cardid");
        }
    }
}
