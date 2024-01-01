using Committees.Application.Features.Committees.Commands.Post;

namespace Committees.Application.Features.Committees.Commands.Put
{
    public class PutCommitteeDto
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
        public List<IFormFile> Attachments { get; set; }
        public List<IFormFile> WorkRules { get; set; }
        public List<ExternalMemberDto> ExternalMembers { get; set; }
        public List<TargetDto> Targets { get; set; }
    }
}
