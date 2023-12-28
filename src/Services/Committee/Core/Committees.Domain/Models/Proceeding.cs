namespace Committees.Models
{
    public class Proceeding : BaseEntity<Guid>
    {
        public Guid CommitteeId { get; set; }
        public Committee Committee { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<ProceedingAttachment> ProceedingAttachments { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }
        public virtual ICollection<ExternalMemberProceeding> ExternalMemberProceedings { get; set; }
		public virtual ICollection<InternalMemberProceeding> InternalMemberProceedings { get; set; }
	}
}
