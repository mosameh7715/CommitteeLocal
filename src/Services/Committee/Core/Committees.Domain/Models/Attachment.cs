namespace Committees.Models
{
    public class Attachment : BaseEntity<Guid>
    {
        public string? Path { get; set; }
	}
}