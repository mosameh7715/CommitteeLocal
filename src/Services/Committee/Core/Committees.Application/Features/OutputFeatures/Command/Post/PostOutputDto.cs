namespace Committees.Application.Features.OutputFeatures.Command.Post
{
    public class PostOutputDto
    {
        public string Name { get; set; }
        public Guid OutputTypeId { get; set; }
        public string Details { get; set; }
        public List<IFormFile> OutputAttachments { get; set; }
        public Guid CommitteeId { get; set; }
        public Guid MeetingId { get; set; }
    }
}
