namespace Committees.Application.Features.ProceedingFeatures.Command.Post
{
    public class PostProceedingCommand : IRequest<ResponseDTO>
    {
        public PostProceedingDto ProceedingDto { get; set; }
    }
}
