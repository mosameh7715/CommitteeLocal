namespace Committees.Application.Features.Committees.Commands.UpdateCommitteesStatus
{
    public class UpdateCommitteesStatusCommand : IRequest<ResponseDTO>
    {
        public Guid CommitteeId { get; set; }
        public CommitteesStatus CommitteesStatus { get; set; }
        public string GroupNotes { get; set; }
    }

}
