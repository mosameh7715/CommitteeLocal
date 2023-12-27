namespace Committees.Application.Features.CommitteeFeatures.Command.Post
{
    public class PostCommitteeCommand : IRequest<ResponseDTO>
    {
        public PostCommitteeDto CommitteeDto { get; set; }
    }
}
