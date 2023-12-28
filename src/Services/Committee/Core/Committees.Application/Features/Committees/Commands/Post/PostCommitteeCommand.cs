namespace Committees.Application.Features.Committees.Commands.Post
{
    public class PostCommitteeCommand : IRequest<ResponseDTO>
    {
        public PostCommitteeDto CommitteeDto { get; set; }
    }
}
