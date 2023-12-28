namespace Committees.Application.Features.CommitteeApprovals.Query.GetById
{
	public class CommitteeApprovalByIdDto
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
		public CommitteeTime CommitteeTime { get; set; }
		public bool HasLegalDocument { get; set; }
		public string WorkRule { get; set; }
		public string? LegalDocument { get; set; }
		public string Missions { get; set; }
		public CommitteesStatus CommitteesStatus { get; set; }
		public List<string> Attachments { get; set; }
		public List<Guid> ExternalMembers { get; set; }
		public List<Guid> InternalMembers { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}
