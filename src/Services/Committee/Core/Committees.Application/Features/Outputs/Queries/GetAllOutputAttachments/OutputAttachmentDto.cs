namespace Committees.Application.Features.Outputs.Queries.GetAllOutputAttachments
{
	public class OutputAttachmentDto
	{
		public Guid Id { get; set; }
		public string Path { get; set; }
		public Guid CreatedBy { set; get; }
		public DateTime CreatedOn { set; get; }
	}
}
