using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Committees.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCommitteeInternalTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommitteeInternalMember_Committees_CommitteeId",
                table: "CommitteeInternalMember");

            migrationBuilder.DropForeignKey(
                name: "FK_CommitteeInternalMember_InternalMembers_InternalMemberId",
                table: "CommitteeInternalMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommitteeInternalMember",
                table: "CommitteeInternalMember");

            migrationBuilder.RenameTable(
                name: "CommitteeInternalMember",
                newName: "CommitteeInternalMembers");

            migrationBuilder.RenameIndex(
                name: "IX_CommitteeInternalMember_InternalMemberId",
                table: "CommitteeInternalMembers",
                newName: "IX_CommitteeInternalMembers_InternalMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_CommitteeInternalMember_CommitteeId",
                table: "CommitteeInternalMembers",
                newName: "IX_CommitteeInternalMembers_CommitteeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommitteeInternalMembers",
                table: "CommitteeInternalMembers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommitteeInternalMembers_Committees_CommitteeId",
                table: "CommitteeInternalMembers",
                column: "CommitteeId",
                principalTable: "Committees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommitteeInternalMembers_InternalMembers_InternalMemberId",
                table: "CommitteeInternalMembers",
                column: "InternalMemberId",
                principalTable: "InternalMembers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommitteeInternalMembers_Committees_CommitteeId",
                table: "CommitteeInternalMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_CommitteeInternalMembers_InternalMembers_InternalMemberId",
                table: "CommitteeInternalMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommitteeInternalMembers",
                table: "CommitteeInternalMembers");

            migrationBuilder.RenameTable(
                name: "CommitteeInternalMembers",
                newName: "CommitteeInternalMember");

            migrationBuilder.RenameIndex(
                name: "IX_CommitteeInternalMembers_InternalMemberId",
                table: "CommitteeInternalMember",
                newName: "IX_CommitteeInternalMember_InternalMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_CommitteeInternalMembers_CommitteeId",
                table: "CommitteeInternalMember",
                newName: "IX_CommitteeInternalMember_CommitteeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommitteeInternalMember",
                table: "CommitteeInternalMember",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommitteeInternalMember_Committees_CommitteeId",
                table: "CommitteeInternalMember",
                column: "CommitteeId",
                principalTable: "Committees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommitteeInternalMember_InternalMembers_InternalMemberId",
                table: "CommitteeInternalMember",
                column: "InternalMemberId",
                principalTable: "InternalMembers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
