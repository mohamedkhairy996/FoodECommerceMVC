using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayerEF.Migrations
{
    /// <inheritdoc />
    public partial class addMessageHeader01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AdminMsg",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminMsg",
                table: "Messages");
        }
    }
}
