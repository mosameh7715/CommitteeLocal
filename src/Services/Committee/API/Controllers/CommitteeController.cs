using Committees.Application.Features.ProceedingFeatures.Command.PostProceedingMembers;

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
        [Route("PostCommittee")]
        public Task<ResponseDTO> PostCommittee([FromForm] PostCommitteeDto postCommitteeDto)
        {
            return _mediator.Send(new PostCommitteeCommand { CommitteeDto = postCommitteeDto });
        }

        [HttpGet, Route("GetCommitteeById/{committeeId}")]
        public async Task<ResponseDTO> GetCommitteeById(Guid committeeId) => await _mediator.Send(new GetCommitteeByIdQuery(committeeId));

        [HttpGet, Route("GetCommitteeTimeTable/{committeeId}")]
        public async Task<ResponseDTO> GetCommitteeTimeTable(Guid committeeId) => await _mediator.Send(new GetCommitteeTimeTableQuery(committeeId));

        [HttpPut("PutCommittee/{committeeId}")]
        public Task<ResponseDTO> PutCommittee([FromForm] PutCommitteeDto putCommitteeDto, Guid committeeId)
        {
            return _mediator.Send(new PutCommitteeCommand { CommitteeId = committeeId, CommitteeDto = putCommitteeDto });
        }

        [HttpPut("ChangeCommitteeStatus/{committeeId}")]
        public async Task<ResponseDTO> ChangeCommitteeStatus([FromRoute] Guid committeeId, [FromBody] UpdateCommitteesStatusCommand command)
        {
            command.CommitteeId = committeeId;
            return await _mediator.Send(command);
        }

        [HttpDelete("DeleteCommittee/{committeeId}")]
        public Task<ResponseDTO> DeleteCommittee(Guid committeeId)
        {
            return _mediator.Send(new DeleteCommitteeCommand() { CommitteeId = committeeId });
        }

        [HttpPost]
        [Route("PostMeeting")]
        public Task<ResponseDTO> PostMeeting([FromForm] PostMeetingDto postMeetingDto)
        {
            return _mediator.Send(new PostMeetingCommand { MeetingDto = postMeetingDto });
        }

        [HttpPost]
        [Route("PostProceeding")]
        public Task<ResponseDTO> PostProceeding([FromForm] PostProceedingDto postProceedingDto)
        {
            return _mediator.Send(new PostProceedingCommand { ProceedingDto = postProceedingDto });
        }

        [HttpPost]
        [Route("PostProceedingMembers")]
        public Task<ResponseDTO> PostProceedingMembers([FromBody] PostProceedingMembersDto postProceedingMembersDto)
        {
            return _mediator.Send(new PostProceedingMembersCommand { MembersDto = postProceedingMembersDto });
        }

        [HttpPost]
        [Route("PostOutput")]
        public Task<ResponseDTO> PostOutput([FromForm] PostOutputDto postOutputDto)
        {
            return _mediator.Send(new PostOutputCommand { OutputDto = postOutputDto });
        }
    }
}
