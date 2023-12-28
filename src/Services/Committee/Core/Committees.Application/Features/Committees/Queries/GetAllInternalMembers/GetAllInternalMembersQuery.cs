namespace Committees.Application.Features.Committees.Queries.GetAllInternalMembers
{
	public class GetAllInternalMembersQuery : IRequest<ResponseDTO>
	{
        public Guid CommitteeId { get; set; }
    }
}
