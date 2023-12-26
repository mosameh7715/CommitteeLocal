namespace Committees.Domain.Models
{
	public class CommitteeAttachment : Attachment
	{
		public Guid CommitteeId { get; set; }
		public Committee Committee { get; set; }
	}
}
