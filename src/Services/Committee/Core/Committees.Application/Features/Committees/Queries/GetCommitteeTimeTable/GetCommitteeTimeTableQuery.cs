namespace Committees.Application.Features.Committees.Queries.GetCommitteeTimeTable
{
    public class GetCommitteeTimeTableQuery : IRequest<ResponseDTO>
    {
        public Guid CommitteeId { get; set; }

        public GetCommitteeTimeTableQuery(Guid committeeId)
        {
            CommitteeId = committeeId;
        }
    }
}
