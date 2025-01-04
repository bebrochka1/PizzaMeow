using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaMeow.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHashed",
                value: "$2a$11$ZZOaoPaxUBvkyFsopqlQpeyOtZcH2x20foaA50i23wiPne0AYi8vi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHashed",
                value: "$2a$11$86pb2imtHTRSl6CzKG2h7OU2iD04A6EsreQhED0l82XsBY1AFiMQy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHashed",
                value: "$2a$11$/FJX48REPiVL1c7ThK2ywOFBjUAMRVhoAg9y.LTveN9MS1O2DvVsq");
        }
    }
}
