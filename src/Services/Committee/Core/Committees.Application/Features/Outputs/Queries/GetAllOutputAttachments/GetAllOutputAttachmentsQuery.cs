namespace Committees.Application.Features.Outputs.Queries.GetAllOutputAttachments
{
	public class GetAllOutputAttachmentsQuery : IRequest<ResponseDTO>
	{
        public Guid OutputId { get; set; }
    }
}
