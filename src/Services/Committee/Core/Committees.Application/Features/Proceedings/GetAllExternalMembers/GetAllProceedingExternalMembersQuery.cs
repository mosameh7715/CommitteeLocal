namespace Committees.Application.Features.Proceedings.GetAllExternalMembers
{
	public class GetAllProceedingExternalMembersQuery : IRequest<ResponseDTO>
	{
        public Guid ProceedingId { get; set; }
    }
}
