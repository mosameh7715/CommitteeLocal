using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Committees.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addExAndInCommitteeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommitteeInternalMember_Committees_CommitteesId",
                table: "CommitteeInternalMember");

            migrationBuilder.DropForeignKey(
                name: "FK_CommitteeInternalMember_InternalMembers_InternalMembersUserId",
                table: "CommitteeInternalMember");

            migrationBuilder.DropForeignKey(
                name: "FK_ExternalMemberProceeding_ExternalMembers_ExternalMembersId",
                table: "ExternalMemberProceeding");

            migrationBuilder.DropForeignKey(
                name: "FK_ExternalMemberProceeding_Proceedings_ProceedingsId",
                table: "ExternalMemberProceeding");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalMemberProceeding",
                table: "ExternalMemberProceeding");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommitteeInternalMember",
                table: "CommitteeInternalMember");

            migrationBuilder.RenameColumn(
                name: "ProceedingsId",
                table: "ExternalMemberProceeding",
                newName: "ProceedingId");

            migrationBuilder.RenameColumn(
                name: "ExternalMembersId",
                table: "ExternalMemberProceeding",
                newName: "ExternalMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_ExternalMemberProceeding_ProceedingsId",
                table: "ExternalMemberProceeding",
                newName: "IX_ExternalMemberProceeding_ProceedingId");

            migrationBuilder.RenameColumn(
                name: "InternalMembersUserId",
                table: "CommitteeInternalMember",
                newName: "InternalMemberId");

            migrationBuilder.RenameColumn(
                name: "CommitteesId",
                table: "CommitteeInternalMember",
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_CommitteeInternalMember_InternalMembersUserId",
                table: "CommitteeInternalMember",
                newName: "IX_CommitteeInternalMember_InternalMemberId");

            migrationBuilder.AddColumn<bool>(
                name: "IsAttend",
                table: "InternalMemberProceedings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ExternalMemberProceeding",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "ExternalMemberProceeding",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ExternalMemberProceeding",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsAttend",
                table: "ExternalMemberProceeding",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ExternalMemberProceeding",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "ExternalMemberProceeding",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "ExternalMemberProceeding",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "ExternalMemberProceeding",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CommitteeInternalMember",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CommitteeId",
                table: "CommitteeInternalMember",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "CommitteeInternalMember",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CommitteeInternalMember",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "CommitteeInternalMember",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "CommitteeInternalMember",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "CommitteeInternalMember",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalMemberProceeding",
                table: "ExternalMemberProceeding",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommitteeInternalMember",
                table: "CommitteeInternalMember",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalMemberProceeding_ExternalMemberId",
                table: "ExternalMemberProceeding",
                column: "ExternalMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_CommitteeInternalMember_CommitteeId",
                table: "CommitteeInternalMember",
                column: "CommitteeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalMemberProceeding_ExternalMembers_ExternalMemberId",
                table: "ExternalMemberProceeding",
                column: "ExternalMemberId",
                principalTable: "ExternalMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalMemberProceeding_Proceedings_ProceedingId",
                table: "ExternalMemberProceeding",
                column: "ProceedingId",
                principalTable: "Proceedings",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommitteeInternalMember_Committees_CommitteeId",
                table: "CommitteeInternalMember");

            migrationBuilder.DropForeignKey(
                name: "FK_CommitteeInternalMember_InternalMembers_InternalMemberId",
                table: "CommitteeInternalMember");

            migrationBuilder.DropForeignKey(
                name: "FK_ExternalMemberProceeding_ExternalMembers_ExternalMemberId",
                table: "ExternalMemberProceeding");

            migrationBuilder.DropForeignKey(
                name: "FK_ExternalMemberProceeding_Proceedings_ProceedingId",
                table: "ExternalMemberProceeding");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalMemberProceeding",
                table: "ExternalMemberProceeding");

            migrationBuilder.DropIndex(
                name: "IX_ExternalMemberProceeding_ExternalMemberId",
                table: "ExternalMemberProceeding");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommitteeInternalMember",
                table: "CommitteeInternalMember");

            migrationBuilder.DropIndex(
                name: "IX_CommitteeInternalMember_CommitteeId",
                table: "CommitteeInternalMember");

            migrationBuilder.DropColumn(
                name: "IsAttend",
                table: "InternalMemberProceedings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ExternalMemberProceeding");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ExternalMemberProceeding");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ExternalMemberProceeding");

            migrationBuilder.DropColumn(
                name: "IsAttend",
                table: "ExternalMemberProceeding");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ExternalMemberProceeding");

            migrationBuilder.DropColumn(
                name: "State",
                table: "ExternalMemberProceeding");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ExternalMemberProceeding");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "ExternalMemberProceeding");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CommitteeInternalMember");

            migrationBuilder.DropColumn(
                name: "CommitteeId",
                table: "CommitteeInternalMember");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "CommitteeInternalMember");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CommitteeInternalMember");

            migrationBuilder.DropColumn(
                name: "State",
                table: "CommitteeInternalMember");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "CommitteeInternalMember");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "CommitteeInternalMember");

            migrationBuilder.RenameColumn(
                name: "ProceedingId",
                table: "ExternalMemberProceeding",
                newName: "ProceedingsId");

            migrationBuilder.RenameColumn(
                name: "ExternalMemberId",
                table: "ExternalMemberProceeding",
                newName: "ExternalMembersId");

            migrationBuilder.RenameIndex(
                name: "IX_ExternalMemberProceeding_ProceedingId",
                table: "ExternalMemberProceeding",
                newName: "IX_ExternalMemberProceeding_ProceedingsId");

            migrationBuilder.RenameColumn(
                name: "InternalMemberId",
                table: "CommitteeInternalMember",
                newName: "InternalMembersUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "CommitteeInternalMember",
                newName: "CommitteesId");

            migrationBuilder.RenameIndex(
                name: "IX_CommitteeInternalMember_InternalMemberId",
                table: "CommitteeInternalMember",
                newName: "IX_CommitteeInternalMember_InternalMembersUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalMemberProceeding",
                table: "ExternalMemberProceeding",
                columns: new[] { "ExternalMembersId", "ProceedingsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommitteeInternalMember",
                table: "CommitteeInternalMember",
                columns: new[] { "CommitteesId", "InternalMembersUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CommitteeInternalMember_Committees_CommitteesId",
                table: "CommitteeInternalMember",
                column: "CommitteesId",
                principalTable: "Committees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommitteeInternalMember_InternalMembers_InternalMembersUserId",
                table: "CommitteeInternalMember",
                column: "InternalMembersUserId",
                principalTable: "InternalMembers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalMemberProceeding_ExternalMembers_ExternalMembersId",
                table: "ExternalMemberProceeding",
                column: "ExternalMembersId",
                principalTable: "ExternalMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalMemberProceeding_Proceedings_ProceedingsId",
                table: "ExternalMemberProceeding",
                column: "ProceedingsId",
                principalTable: "Proceedings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
