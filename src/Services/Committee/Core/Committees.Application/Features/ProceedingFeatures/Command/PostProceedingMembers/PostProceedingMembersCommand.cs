namespace Committees.Application.Features.ProceedingFeatures.Command.PostProceedingMembers
{
    public class PostProceedingMembersCommand : IRequest<ResponseDTO>
    {
        public Guid ProceedingId { get; set; }
        public PostProceedingMembersDto MembersDto { get; set; }
    }
}
