namespace Committees.Application.Features.Proceedings.GetAllInternalMembers
{
	public class GetAllProceedingInternalMembersQuery : IRequest<ResponseDTO>
	{
		public Guid ProceedingId { get; set; }
	}
}
