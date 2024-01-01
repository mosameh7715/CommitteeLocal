namespace Committees.Application.Features.Proceedings.GetById
{
	public class GetProceedingByIdQuery : IRequest<ResponseDTO>
	{
        public Guid ProceedingId { get; set; }
    }
}
