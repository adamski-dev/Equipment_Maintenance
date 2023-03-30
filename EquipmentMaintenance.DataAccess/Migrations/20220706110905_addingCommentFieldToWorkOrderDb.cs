using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentMaintenance.DataAccess.Migrations
{
    public partial class addingCommentFieldToWorkOrderDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "WorkOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "WorkOrders");
        }
    }
}
