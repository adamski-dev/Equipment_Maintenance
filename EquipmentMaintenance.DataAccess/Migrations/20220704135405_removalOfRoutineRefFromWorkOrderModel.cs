using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentMaintenance.DataAccess.Migrations
{
    public partial class removalOfRoutineRefFromWorkOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routines_WorkOrders_WorkOrderId",
                table: "Routines");

            migrationBuilder.DropIndex(
                name: "IX_Routines_WorkOrderId",
                table: "Routines");

            migrationBuilder.DropColumn(
                name: "WorkOrderId",
                table: "Routines");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkOrderId",
                table: "Routines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Routines_WorkOrderId",
                table: "Routines",
                column: "WorkOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routines_WorkOrders_WorkOrderId",
                table: "Routines",
                column: "WorkOrderId",
                principalTable: "WorkOrders",
                principalColumn: "Id");
        }
    }
}
