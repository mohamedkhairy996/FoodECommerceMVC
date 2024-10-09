using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayerEF.Migrations
{
    /// <inheritdoc />
    public partial class addMessageHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_FromUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ToUserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_FromUserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ToUserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "FromUserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ToUserId",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "HeaderId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "messageHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ToUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messageHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_messageHeaders_AspNetUsers_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_messageHeaders_AspNetUsers_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_HeaderId",
                table: "Messages",
                column: "HeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_messageHeaders_FromUserId",
                table: "messageHeaders",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_messageHeaders_ToUserId",
                table: "messageHeaders",
                column: "ToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_messageHeaders_HeaderId",
                table: "Messages",
                column: "HeaderId",
                principalTable: "messageHeaders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_messageHeaders_HeaderId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "messageHeaders");

            migrationBuilder.DropIndex(
                name: "IX_Messages_HeaderId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "HeaderId",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "FromUserId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToUserId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FromUserId",
                table: "Messages",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ToUserId",
                table: "Messages",
                column: "ToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_FromUserId",
                table: "Messages",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ToUserId",
                table: "Messages",
                column: "ToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
