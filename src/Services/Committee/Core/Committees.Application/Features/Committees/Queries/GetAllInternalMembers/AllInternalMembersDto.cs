namespace Committees.Application.Features.Committees.Queries.GetAllInternalMembers
{
	public class AllInternalMembersDto
	{
		public Guid UserId { get; set; }
		public string PermissionNameAr { get; set; }
		public string PermissionNameEn { get; set; }
	}
}
