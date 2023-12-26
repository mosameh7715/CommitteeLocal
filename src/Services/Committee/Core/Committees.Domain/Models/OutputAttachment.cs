namespace Committees.Domain.Models
{
	public class OutputAttachment : Attachment
	{
		public Guid OutputId { get; set; }
		public Output Output { get; set; }
	}
}
