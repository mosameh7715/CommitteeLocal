namespace Committees.Application.Features.Committees.Queries.GetAll
{
	public class GetAllCommitteesQuery : IRequest<ResponseDTO>
	{
        public string? SearchTerm { get; set; }
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
	}
}
