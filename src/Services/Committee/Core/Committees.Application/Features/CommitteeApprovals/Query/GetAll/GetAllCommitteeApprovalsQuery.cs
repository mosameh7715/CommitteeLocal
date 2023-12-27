namespace Committees.Application.Features.CommitteeApprovals.Query.GetAll
{
	public class GetAllCommitteeApprovalsQuery : IRequest<ResponseDTO>
	{
		public string? SearchTerm { get; set; }
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
	}
}
