using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learning_ms.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    otp_code = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    resend_otp_code = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    otp_expiration_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    access_token = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    refresh_token = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    refresh_token_expiration_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    magic_link_token = table.Column<string>(type: "text", nullable: false),
                    resend_magic_link_token = table.Column<string>(type: "text", nullable: false),
                    verify_magic_link_token = table.Column<string>(type: "text", nullable: false),
                    forgot_password = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    forgot_password_expiration_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    reset_password = table.Column<string>(type: "text", nullable: false),
                    block_user = table.Column<bool>(type: "boolean", nullable: false),
                    un_block_user = table.Column<bool>(type: "boolean", nullable: false),
                    magic_link_token_expiration_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    firebase_uid = table.Column<string>(type: "text", nullable: true),
                    firebase_provider = table.Column<string>(type: "text", nullable: true),
                    firebase_id_token = table.Column<string>(type: "text", nullable: true),
                    failed_login_attempts = table.Column<int>(type: "integer", nullable: false),
                    firebase_refresh_token = table.Column<string>(type: "text", nullable: true),
                    is_firebase_email_verified = table.Column<bool>(type: "boolean", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    is_phone_number_verified = table.Column<bool>(type: "boolean", nullable: false),
                    photo_url = table.Column<string>(type: "text", nullable: true),
                    firebase_display_name = table.Column<string>(type: "text", nullable: true),
                    firebase_last_login_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    firebase_created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_firebase_disabled = table.Column<bool>(type: "boolean", nullable: false),
                    firebase_tenant_id = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "Users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
