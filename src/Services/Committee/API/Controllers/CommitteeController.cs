namespace Committees.API.Controllers
{
    public class CommitteeController : BaseController
    {
        private readonly IMediator _mediator;

        public CommitteeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("PostPermission")]
        public async Task<ResponseDTO> PostPermission(PostPermissionCommand command) => await _mediator.Send(command);

        [HttpGet]
        [Route("GetAllPermissions")]
        public async Task<ResponseDTO> GetAllPermissions()
        {
            var query = new GetAllPermissionsQuery();
            return await _mediator.Send(query);
        }

        [HttpPut]
        [Route("PutPermission/{id}")]
        public async Task<ResponseDTO> PutPermission([FromRoute] Guid id, PutPermissionCommand command)
        {
            command.PermissionId = id;
            return await _mediator.Send(command);
        }

        [HttpDelete]
        [Route("DeletePermission/{id}")]
        public async Task<ResponseDTO> DeletePermission(Guid id) => await _mediator.Send(new DeletePermissionCommand(id));
    }
}
