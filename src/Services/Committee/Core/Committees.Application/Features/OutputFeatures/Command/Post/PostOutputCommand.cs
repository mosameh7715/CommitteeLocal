namespace Committees.Application.Features.OutputFeatures.Command.Post
{
    public class PostOutputCommand : IRequest<ResponseDTO>
    {
        public PostOutputDto OutputDto { get; set; }
    }
}
