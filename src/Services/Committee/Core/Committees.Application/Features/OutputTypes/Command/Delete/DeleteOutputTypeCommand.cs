namespace Committees.Application.Features.OutputTypes.Command.Delete
{
    public class DeleteOutputTypeCommand : IRequest<ResponseDTO>
    {
        public DeleteOutputTypeCommand(Guid id)
        {
            OutputTypeId = id;
        }

        public Guid OutputTypeId { get; set; }
    }
}
