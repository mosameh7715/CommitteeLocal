namespace Committees.Domain.Models
{
	public class CommitteeInternalMember : BaseEntity<Guid>
	{
        public Guid InternalMemberId { get; set; }
        public InternalMember InternalMember { get; set; }
		public Guid CommitteeId { get; set; }
		public Committee Committee { get; set; }
	}
}
