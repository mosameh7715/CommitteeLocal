namespace Committees.Application.Features.CommitteeApprovals.Query.GetAll
{
	public class AllCommitteeApprovalDto
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
		public CommitteeTime CommitteeTime { get; set; }
		public string Missions { get; set; }
		public DateTime CreatedOn { get; set; }
		public CommitteesStatus CommitteesStatus { get; set; }
	}
}
