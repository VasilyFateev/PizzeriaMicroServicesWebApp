using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabasesAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixHashedPasswordColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_email_hashed_password",
                table: "user");

            migrationBuilder.DropIndex(
                name: "ix_user_phone_number_hashed_password",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "hashed_passsword",
                table: "user",
                newName: "hashed_password");

            migrationBuilder.CreateIndex(
                name: "ix_user_email",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_phone_number",
                table: "user",
                column: "phone_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_email",
                table: "user");

            migrationBuilder.DropIndex(
                name: "ix_user_phone_number",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "hashed_password",
                table: "user",
                newName: "hashed_passsword");

            migrationBuilder.CreateIndex(
                name: "ix_user_email_hashed_password",
                table: "user",
                columns: new[] { "email", "hashed_passsword" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_phone_number_hashed_password",
                table: "user",
                columns: new[] { "phone_number", "hashed_passsword" },
                unique: true);
        }
    }
}
