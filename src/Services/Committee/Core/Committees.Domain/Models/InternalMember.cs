namespace Committees.Domain.Models
{
	public class InternalMember : BaseEntity<Guid>
	{
		[Key]
        public Guid UserId { get; set; }
		public Guid PermissionId { get; set; }
		public Permission Permission { get; set; }
		public ICollection<Committee> Committees { get; set; }
		public ICollection<InternalMemberMeeting> InternalMemberMeetings { get; set; }
		public virtual ICollection<InternalMemberProceeding> InternalMemberProceedings { get; set; }
	}
}
