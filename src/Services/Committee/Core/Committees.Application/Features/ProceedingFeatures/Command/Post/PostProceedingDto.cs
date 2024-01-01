namespace Committees.Application.Features.ProceedingFeatures.Command.Post
{
    public class PostProceedingDto
    {
        public string Name { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public List<IFormFile> ProceedingAttachments { get; set; }
        public Guid CommitteeId { get; set; }
        public Guid MeetingId { get; set; }
    }
}
