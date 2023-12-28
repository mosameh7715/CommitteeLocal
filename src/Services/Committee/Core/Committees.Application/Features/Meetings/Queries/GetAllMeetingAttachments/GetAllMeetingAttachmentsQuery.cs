namespace Committees.Application.Features.Meetings.Queries.GetAllMeetingAttachments
{
	public class GetAllMeetingAttachmentsQuery : IRequest<ResponseDTO>
	{
        public Guid MeetingId { get; set; }
    }
}
