namespace Committees.Application.Features.Committees.Queries.GetAllExternalMembers
{
	public class GetAllExternalMembersQuery : IRequest<ResponseDTO>
	{
        public Guid CommitteeId { get; set; }
    }
}
