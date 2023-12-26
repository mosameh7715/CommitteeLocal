namespace Committees.API.Controllers
{
    public class OutputController : BaseController
    {
        private readonly IMediator _mediator;

        public OutputController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("PostOutputType")]
        public async Task<ResponseDTO> PostOutputType([FromBody] PostOutputTypeCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("GetAllOutputTypes")]
        public async Task<ResponseDTO> GetAllOutputTypes()
        {
            var query = new GetAllOutputTypesQuery();
            return await _mediator.Send(query);
        }

        [HttpPut("PutOutputType/{outputTypeId}")]
        public async Task<ResponseDTO> PutOutputType([FromRoute] Guid outputTypeId, [FromBody] PutOutputTypeCommand command)
        {
            command.OutputTypeId = outputTypeId;
            return await _mediator.Send(command);
        }

        [HttpDelete("DeleteOutputType/{outputTypeId}")]
        public async Task<ResponseDTO> DeleteOutputType(Guid outputTypeId)
        {
            var command = new DeleteOutputTypeCommand(outputTypeId);
            return await _mediator.Send(command);
        }
    }
}
