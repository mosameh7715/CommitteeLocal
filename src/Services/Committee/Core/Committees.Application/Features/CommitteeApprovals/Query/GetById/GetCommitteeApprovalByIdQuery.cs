namespace Committees.Application.Features.CommitteeApprovals.Query.GetById
{
	public class GetCommitteeApprovalByIdQuery : IRequest<ResponseDTO>
	{
        public Guid SubKnowledgeId { get; set; }
    }
}
