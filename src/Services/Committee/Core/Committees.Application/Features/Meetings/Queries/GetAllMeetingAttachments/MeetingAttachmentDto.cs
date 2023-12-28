namespace Committees.Application.Features.Meetings.Queries.GetAllMeetingAttachments
{
	public class MeetingAttachmentDto
	{
        public Guid Id { get; set; }
        public string Path { get; set; }
		public Guid CreatedBy { set; get; }
		public DateTime CreatedOn { set; get; }
	}
}
