namespace Committees.Application.Features.ProceedingFeatures.Command.PostProceedingMembers
{
    public class PostProceedingMembersDto
    {
        public List<Guid> InternalMemberIds { get; set; }
        public List<Guid> ExternalMemberIds { get; set; }
    }
}
