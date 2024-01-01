namespace Committees.Application.Features.MeetingsFeatures.Command.Post
{
    public class PostMeetingCommand : IRequest<ResponseDTO>
    {
        public PostMeetingDto MeetingDto { get; set; }
    }
}
