namespace Committees.Models
{
    public class ProceedingAttachment : Attachment
    {
        public Guid ProceedingId { get; set; }
        public Proceeding Proceeding { get; set; }
    }
}
