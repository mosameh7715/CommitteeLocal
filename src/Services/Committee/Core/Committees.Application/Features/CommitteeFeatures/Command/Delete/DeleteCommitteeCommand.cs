namespace Committees.Application.Features.CommitteeFeatures.Command.Delete
{
    public class DeleteCommitteeCommand : IRequest<ResponseDTO>
    {
        public Guid CommitteeId { get; set; }
    }
}
