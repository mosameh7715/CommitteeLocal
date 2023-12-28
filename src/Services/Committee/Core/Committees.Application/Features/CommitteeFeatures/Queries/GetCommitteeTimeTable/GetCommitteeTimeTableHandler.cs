namespace Committees.Application.Features.CommitteeFeatures.Queries.GetCommitteeTimeTable
{
    public class GetCommitteeTimeTableHandler : IRequestHandler<GetCommitteeTimeTableQuery, ResponseDTO>
    {
        private readonly IGRepository<Meeting> _meetingRepository;
        private readonly IResponseHelper _responseHelper;
        private readonly IMapper _mapper;

        public GetCommitteeTimeTableHandler
        (
            IGRepository<Meeting> meetingRepository,
            IResponseHelper responseHelper,
            IMapper mapper
        )
        {
            _meetingRepository = meetingRepository;
            _responseHelper = responseHelper;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> Handle(GetCommitteeTimeTableQuery request, CancellationToken cancellationToken)
        {
            var meetings = await _meetingRepository.GetAllAsync(m => m.CommitteeId == request.CommitteeId);

            var meetingDTOs = _mapper.Map<List<MeetingDTO>>(meetings);

            return _responseHelper.RetrievedSuccessfully(meetingDTOs, "CommitteeMeetingsRetrievedSuccessfully!");
        }
    }
}
