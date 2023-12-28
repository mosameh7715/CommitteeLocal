namespace Committees.Application.Features.OutputFeatures.Command.Post
{
    public class PostOutputCommand : IRequest<ResponseDTO>
    {
        public Guid CommitteeId { get; set; }
        public PostOutputDto OutputDto { get; set; }
    }
}
