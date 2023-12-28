namespace Committees.Application.Features.MeetingsFeatures.Command.Post
{
    public class PostMeetingDto
    {
        public string Rules { get; set; }
        public string Location { get; set; }
        public MeetingStatus MeetingStatus { get; set; }
        public bool HasExpertAssist { get; set; }
        public DateTime MeetingDate { get; set; }
        public List<IFormFile> MeetingAttachments { get; set; }
    }
}
