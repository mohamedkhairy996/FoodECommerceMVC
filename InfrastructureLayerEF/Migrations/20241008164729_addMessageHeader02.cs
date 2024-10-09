using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayerEF.Migrations
{
    /// <inheritdoc />
    public partial class addMessageHeader02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messageHeaders_AspNetUsers_FromUserId",
                table: "messageHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_messageHeaders_AspNetUsers_ToUserId",
                table: "messageHeaders");

            migrationBuilder.RenameColumn(
                name: "ToUserId",
                table: "messageHeaders",
                newName: "User2_Id");

            migrationBuilder.RenameColumn(
                name: "FromUserId",
                table: "messageHeaders",
                newName: "User1_Id");

            migrationBuilder.RenameIndex(
                name: "IX_messageHeaders_ToUserId",
                table: "messageHeaders",
                newName: "IX_messageHeaders_User2_Id");

            migrationBuilder.RenameIndex(
                name: "IX_messageHeaders_FromUserId",
                table: "messageHeaders",
                newName: "IX_messageHeaders_User1_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_messageHeaders_AspNetUsers_User1_Id",
                table: "messageHeaders",
                column: "User1_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_messageHeaders_AspNetUsers_User2_Id",
                table: "messageHeaders",
                column: "User2_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messageHeaders_AspNetUsers_User1_Id",
                table: "messageHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_messageHeaders_AspNetUsers_User2_Id",
                table: "messageHeaders");

            migrationBuilder.RenameColumn(
                name: "User2_Id",
                table: "messageHeaders",
                newName: "ToUserId");

            migrationBuilder.RenameColumn(
                name: "User1_Id",
                table: "messageHeaders",
                newName: "FromUserId");

            migrationBuilder.RenameIndex(
                name: "IX_messageHeaders_User2_Id",
                table: "messageHeaders",
                newName: "IX_messageHeaders_ToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_messageHeaders_User1_Id",
                table: "messageHeaders",
                newName: "IX_messageHeaders_FromUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_messageHeaders_AspNetUsers_FromUserId",
                table: "messageHeaders",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_messageHeaders_AspNetUsers_ToUserId",
                table: "messageHeaders",
                column: "ToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
