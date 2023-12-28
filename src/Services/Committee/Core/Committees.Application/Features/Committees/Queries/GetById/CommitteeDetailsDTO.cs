namespace Committees.Application.Features.Committees.Queries.GetById
{
    public class CommitteeDetailsDTO
    {
        public Guid Id { get; set; }
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
        public DateTime CreatedOn { get; set; }
        public List<string> Attachments { get; set; }
        public List<string> WorkRules { get; set; }
        public List<string> Targets { get; set; }
    }
}
