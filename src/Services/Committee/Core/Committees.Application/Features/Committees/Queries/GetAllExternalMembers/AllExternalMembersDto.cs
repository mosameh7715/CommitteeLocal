namespace Committees.Application.Features.Committees.Queries.GetAllExternalMembers
{
	public class AllExternalMembersDto
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
		public string PhoneNumber { get; set; }
		public string Job { get; set; }
		public string DestinationName { get; set; }
		public string Email { get; set; }
		public string PermissionNameAr { get; set; }
		public string PermissionNameEn { get; set; }
	}
}
