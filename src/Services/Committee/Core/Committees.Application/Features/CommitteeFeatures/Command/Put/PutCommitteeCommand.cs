namespace Committees.Application.Features.CommitteeFeatures.Command.Put
{
    public class PutCommitteeCommand : IRequest<ResponseDTO>
    {
        public Guid CommitteeId { get; set; }
        public PutCommitteeDto CommitteeDto { get; set; }
    }
}
