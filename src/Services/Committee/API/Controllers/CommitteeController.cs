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

        //Committee
        [HttpPost]
        [Route("PostCommittee")]
        public Task<ResponseDTO> PostCommittee([FromForm] PostCommitteeDto postCommitteeDto)
        {
            return _mediator.Send(new PostCommitteeCommand { CommitteeDto = postCommitteeDto });
        }

        [HttpPut("PutCommittee/{committeeId}")]
        public Task<ResponseDTO> PutCommittee([FromForm] PutCommitteeDto putCommitteeDto, Guid committeeId)
        {
            return _mediator.Send(new PutCommitteeCommand { CommitteeId = committeeId, CommitteeDto = putCommitteeDto });
        }

        [HttpDelete("DeleteCommittee/{committeeId}")]
        public Task<ResponseDTO> DeleteCommittee(Guid committeeId)
        {
            return _mediator.Send(new DeleteCommitteeCommand() { CommitteeId = committeeId });
        }
    }
}
