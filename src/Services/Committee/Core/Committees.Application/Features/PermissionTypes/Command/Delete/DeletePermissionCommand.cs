namespace Committees.Application.Features.PermissionTypes.Command.Delete
{
    public class DeletePermissionCommand : IRequest<ResponseDTO>
    {
        public DeletePermissionCommand(Guid id)
        {
            PermissionId = id;
        }

        public Guid PermissionId { get; set; }
    }
}
