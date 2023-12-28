namespace Committees.Application.Features.Committees.Queries.GetAll
{
	public class GetAllCommitteesDto
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
		public CommitteesStatus CommitteesStatus { get; set; }
		public DateTime CreatedOn { get; set; }
		public List<Guid> ExternalMembers { get; set; }
		public List<Guid> InternalMembers { get; set; }
	}
}
