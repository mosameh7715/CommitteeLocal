namespace Committees.Application.Features.CommitteeFeatures.Queries.GetById
{
    public class GetCommitteeByIdQuery : IRequest<ResponseDTO>
    {
        public Guid CommitteeId { get; set; }

        public GetCommitteeByIdQuery(Guid committeeId)
        {
            CommitteeId = committeeId;
        }
    }
}
