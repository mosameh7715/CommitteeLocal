namespace Committees.Application.Features.MeetingsFeatures.Command.Post
{
    public class PostMeetingCommand : IRequest<ResponseDTO>
    {
        public Guid CommitteeId { get; set; }
        public PostMeetingDto MeetingDto { get; set; }
    }
}
