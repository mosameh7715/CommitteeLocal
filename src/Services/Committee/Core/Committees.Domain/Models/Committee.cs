namespace Committees.Models
{
    public class Committee : BaseEntity<Guid>
    {
        public string Name { get; set; }
		public CommitteeTime CommitteeTime { get; set; }
		public string ProjectName { get; set; }
		public string Description { get; set; }
		public bool HasLegalDocument { get; set; }
		public string WorkRule { get; set; }
		public string? LegalDocument { get; set; }
		public string Missions { get; set; }
        public CommitteesStatus CommitteesStatus { get; set; }
        public string Location { get; set; }
		public string Position { get; set; }
        public string? GroupNotes { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }
        public virtual ICollection<CommitteeAttachment> Attachments { get; set; }
		public virtual ICollection<WorkRule> WorkRules { get; set; }
		public virtual ICollection<ExternalMember> ExternalMembers { get; set; }
		public virtual ICollection<CommitteeInternalMember> CommitteeInternalMembers { get; set; }
		public virtual ICollection<Proceeding> Proceedings { get; set; }
        public virtual ICollection<Output> Outputs { get; set; }
		public virtual ICollection<Target> Targets { get; set; }
	}
}
