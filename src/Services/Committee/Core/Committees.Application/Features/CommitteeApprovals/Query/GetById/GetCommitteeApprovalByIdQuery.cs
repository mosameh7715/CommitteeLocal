namespace Committees.Application.Features.CommitteeApprovals.Query.GetById
{
	public class GetCommitteeApprovalByIdQuery : IRequest<ResponseDTO>
	{
        public Guid CommitteeId { get; set; }
    }
}
