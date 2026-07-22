using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learning_ms.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentProfileUserIdAndPendingImageCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "student_profiles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    pending_image_count = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    admission_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    middle_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    profile_picture_url = table.Column<List<string>>(type: "text[]", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: false),
                    gender = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    nationality = table.Column<string>(type: "text", nullable: false),
                    place_of_birth = table.Column<string>(type: "text", nullable: false),
                    hobbies = table.Column<List<string>>(type: "text[]", nullable: false),
                    blood_group = table.Column<string>(type: "text", nullable: true),
                    religion = table.Column<string>(type: "text", nullable: true),
                    current_class = table.Column<string>(type: "text", nullable: false),
                    section = table.Column<string>(type: "text", nullable: false),
                    academic_year = table.Column<string>(type: "text", nullable: false),
                    admission_date = table.Column<DateOnly>(type: "date", nullable: false),
                    father_full_name = table.Column<string>(type: "text", nullable: false),
                    father_phone_number = table.Column<string>(type: "text", nullable: false),
                    father_occupation = table.Column<string>(type: "text", nullable: false),
                    mother_full_name = table.Column<string>(type: "text", nullable: false),
                    mother_phone_number = table.Column<string>(type: "text", nullable: false),
                    mother_occupation = table.Column<string>(type: "text", nullable: false),
                    guardian_full_name = table.Column<string>(type: "text", nullable: true),
                    guardian_relationship = table.Column<string>(type: "text", nullable: true),
                    guardian_phone_number = table.Column<string>(type: "text", nullable: true),
                    guardian_email = table.Column<string>(type: "text", nullable: true),
                    emergency_contact_name = table.Column<string>(type: "text", nullable: false),
                    emergency_contact_phone_number = table.Column<string>(type: "text", nullable: false),
                    emergency_contact_relationship = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    state = table.Column<string>(type: "text", nullable: false),
                    postal_code = table.Column<string>(type: "text", nullable: false),
                    country = table.Column<string>(type: "text", nullable: false),
                    allergies = table.Column<string>(type: "text", nullable: true),
                    medical_conditions = table.Column<string>(type: "text", nullable: true),
                    medications = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    is_graduated = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_student_profiles", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_student_profiles_admission_number",
                table: "student_profiles",
                column: "admission_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_student_profiles_user_id",
                table: "student_profiles",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "student_profiles");
        }
    }
}
