namespace Committees.Domain.Models
{
	public class WorkRule : Attachment
	{
		public Guid CommitteeId { get; set; }
		public Committee Committee { get; set; }
	}
}
