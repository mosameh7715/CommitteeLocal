namespace Committees.Application.Features.Proceedings.GetById
{
	public class GetProceedingByIdDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public List<string> Attachments { get; set; }
		public List<Guid> ExternalMembers { get; set; }
		public List<Guid> InternalMembers { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}
