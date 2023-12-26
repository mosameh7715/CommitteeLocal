namespace Committees.Domain.Models
{
	public class Output : BaseEntity<Guid>
	{
        public string Name { get; set; }
		public Guid OutputTypeId { get; set; }
		public OutputType OutputType { get; set; }
        public string Details { get; set; }
        public ICollection<OutputAttachment> OutputAttachments { get; set; }
		public ICollection<Meeting> Meetings { get; set; }
		public Guid CommitteeId { get; set; }
		public Committee Committee { get; set; }
	}
}
