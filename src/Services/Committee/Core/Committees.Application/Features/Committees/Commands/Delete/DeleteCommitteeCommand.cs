namespace Committees.Application.Features.Committees.Commands.Delete
{
    public class DeleteCommitteeCommand : IRequest<ResponseDTO>
    {
        public Guid CommitteeId { get; set; }
    }
}
