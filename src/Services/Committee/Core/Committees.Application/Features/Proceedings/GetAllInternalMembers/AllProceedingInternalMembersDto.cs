namespace Committees.Application.Features.Proceedings.GetAllInternalMembers
{
	public class AllProceedingInternalMembersDto
	{
		public Guid UserId { get; set; }
		public string PermissionNameAr { get; set; }
		public string PermissionNameEn { get; set; }
		public bool IsAttend { get; set; }
	}
}
