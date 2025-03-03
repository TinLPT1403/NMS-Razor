using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountPassword",
                table: "SystemAccounts",
                newName: "AccountPasswordHash");

            migrationBuilder.AlterColumn<string>(
                name: "AccountName",
                table: "SystemAccounts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "SystemAccounts",
                keyColumn: "AccountId",
                keyValue: -3,
                columns: new[] { "AccountPasswordHash", "AccountRole" },
                values: new object[] { "$2a$11$XhFt4joOsZAtT2U3JZnc4OF16cwVwZ/rA6VMB8wR9UizPs9rf5/we", 1 });

            migrationBuilder.UpdateData(
                table: "SystemAccounts",
                keyColumn: "AccountId",
                keyValue: -2,
                column: "AccountPasswordHash",
                value: "$2a$11$r4p3qfWEXlXdPjUwOpF0DOKoTga8YV7Q9TKKHfX7XNH8GaZV.Mp/m");

            migrationBuilder.UpdateData(
                table: "SystemAccounts",
                keyColumn: "AccountId",
                keyValue: -1,
                columns: new[] { "AccountPasswordHash", "AccountRole" },
                values: new object[] { "$2a$11$rwxlp.y4gjXvIW7IreN0LOpB9RIptTa/AnFIq0CM9GaEAaXcWKhDa", 3 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountPasswordHash",
                table: "SystemAccounts",
                newName: "AccountPassword");

            migrationBuilder.AlterColumn<string>(
                name: "AccountName",
                table: "SystemAccounts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "SystemAccounts",
                keyColumn: "AccountId",
                keyValue: -3,
                columns: new[] { "AccountPassword", "AccountRole" },
                values: new object[] { "123456", 3 });

            migrationBuilder.UpdateData(
                table: "SystemAccounts",
                keyColumn: "AccountId",
                keyValue: -2,
                column: "AccountPassword",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "SystemAccounts",
                keyColumn: "AccountId",
                keyValue: -1,
                columns: new[] { "AccountPassword", "AccountRole" },
                values: new object[] { "123456", 1 });
        }
    }
}
