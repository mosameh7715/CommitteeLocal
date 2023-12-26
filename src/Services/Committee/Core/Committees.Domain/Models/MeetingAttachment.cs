namespace Committees.Models
{
    public class MeetingAttachment : Attachment
    {
        public Guid MeetingId { get; set; }
        public Meeting Meeting { get; set; }
    }
}
