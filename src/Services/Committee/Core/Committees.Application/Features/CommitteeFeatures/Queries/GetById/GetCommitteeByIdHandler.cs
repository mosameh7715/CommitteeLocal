namespace Committees.Application.Features.CommitteeFeatures.Queries.GetById
{
    public class GetCommitteeByIdHandler : IRequestHandler<GetCommitteeByIdQuery, ResponseDTO>
    {
        private readonly IGRepository<Committee> _committeeRepository;
        private readonly IGRepository<CommitteeAttachment> _attachmentRepository;
        private readonly IGRepository<WorkRule> _workRuleRepository;
        private readonly IGRepository<Target> _targetRepository;
        private readonly IResponseHelper _responseHelper;
        private readonly IMapper _mapper;
        public GetCommitteeByIdHandler
        (
            IGRepository<Committee> committeeRepository,
            IGRepository<CommitteeAttachment> attachmentRepository,
            IGRepository<WorkRule> workRuleRepository,
            IGRepository<Target> targetRepository,
            IResponseHelper responseHelper,
            IMapper mapper
        )
        {
            _committeeRepository = committeeRepository;
            _attachmentRepository = attachmentRepository;
            _workRuleRepository = workRuleRepository;
            _targetRepository = targetRepository;
            _responseHelper = responseHelper;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> Handle(GetCommitteeByIdQuery request, CancellationToken cancellationToken)
        {
            var committee = await _committeeRepository.GetFirstAsync(x => x.Id == request.CommitteeId);

            if (committee == null)
            {
                return _responseHelper.NotFound("ThereIsNoCommitteeWithSuchGivenId!");
            }

            var committeeDetails = _mapper.Map<CommitteeDetailsDTO>(committee);

            var attachments = await _attachmentRepository.GetAllAsync(x => x.CommitteeId == committee.Id);
            var workRules = await _workRuleRepository.GetAllAsync(x => x.CommitteeId == committee.Id);
            var targets = await _targetRepository.GetAllAsync(x => x.CommitteeId == committee.Id);

            committeeDetails.Attachments = attachments.Select(x => x.Path).ToList();
            committeeDetails.WorkRules = workRules.Select(x => x.Path).ToList();
            committeeDetails.Targets = targets.Select(target => target.Goal).ToList();

            return _responseHelper.RetrievedSuccessfully(committeeDetails, "CommitteeDetailsRetrievedSuccessfully!");
        }
    }
}
