using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayerEF.Migrations
{
    /// <inheritdoc />
    public partial class addMessageHeader03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "messageHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "messageHeaders");
        }
    }
}
