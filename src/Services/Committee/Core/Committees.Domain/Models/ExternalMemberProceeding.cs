namespace Committees.Domain.Models
{
	public class ExternalMemberProceeding : BaseEntity<Guid>
	{
		public Guid ExternalMemberId { get; set; }
		public ExternalMember ExternalMember { get; set; }
		public Guid ProceedingId { get; set; }
		public Proceeding Proceeding { get; set; }
		public bool IsAttend { get; set; }
	}
}
