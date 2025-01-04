using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaMeow.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentMethodPropertyIntoOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHashed",
                value: "$2a$11$sVvU6hVRrtlNyBQ1fW07zub3zDpvbhhV.y1bjrEsxvfFcqGDbXP5i");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHashed",
                value: "$2a$11$GdU.wGK9W0HpfuhsdaXRdeBzjbaL1DISEC9jwwWQBJ90.tTW0yQdq");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHashed",
                value: "$2a$11$COozmlaoJGTaiyWICs436ebW/.mvwjpPfL/G5shiHVhadqRwY5B7e");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHashed",
                value: "$2a$11$EYMqny5oIWQsHR6hR/996ufPRHbYgrDPyHtljLncxd6EtLDoZrqbi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHashed",
                value: "$2a$11$hOIrOm8DqkmgTbKMv5BkcOblifIi6ZcaSrjYallgs5ec96y0s7pre");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHashed",
                value: "$2a$11$V0WOHMunzgLZbjSAIym4.eG8N3xW.CP9NZOIl5B.3U8pYJKqaSyHS");
        }
    }
}
