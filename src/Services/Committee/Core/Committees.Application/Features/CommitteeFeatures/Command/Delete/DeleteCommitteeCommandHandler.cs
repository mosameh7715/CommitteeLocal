namespace Committees.Application.Features.CommitteeFeatures.Command.Delete
{
    public class DeleteCommitteeCommandHandler : IRequestHandler<DeleteCommitteeCommand, ResponseDTO>
    {
        private readonly IGRepository<Committee> _committeeRepo;
        private readonly IGRepository<CommitteeAttachment> _attachmentRepo;
        private readonly IGRepository<WorkRule> _workRuleRepo;
        private readonly IGRepository<ExternalMember> _externalMemberRepo;
        private readonly IGRepository<Target> _targetRepo;
        private readonly IMapper _mapper;
        private readonly IResponseHelper _responseHelper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseDTO _responseDTO;
        public Guid _loggedInUserId;

        public DeleteCommitteeCommandHandler
        (
            IGRepository<CommitteeAttachment> attachmentRepo,
            IGRepository<ExternalMember> externalMemberRepo,
            IHttpContextAccessor _httpContextAccessor,
            IGRepository<Committee> committeeRepo,
            IGRepository<WorkRule> workRuleRepo,
            IGRepository<Target> targetRepo,
            IResponseHelper responseHelper,
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _externalMemberRepo = externalMemberRepo;
            _attachmentRepo = attachmentRepo;
            _committeeRepo = committeeRepo;
            _workRuleRepo = workRuleRepo;
            _targetRepo = targetRepo;
            _responseHelper = responseHelper;
            _responseDTO = new ResponseDTO();
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggedInUserId = Infrastructure.Helpers.LoggedInUserProvider.GetLoggedInUserId(_httpContextAccessor);
        }

        public async Task<ResponseDTO> Handle(DeleteCommitteeCommand request, CancellationToken cancellationToken)
        {
            var committee = _committeeRepo
                                                   .GetAll(x => x.Id == request.CommitteeId && x.State == State.NotDeleted)
                                                   .Include(x => x.Attachments.Where(a => a.State == State.NotDeleted))
                                                   .Include(x => x.WorkRules.Where(w => w.State == State.NotDeleted))
                                                   .Include(x => x.Targets.Where(t => t.State == State.NotDeleted))
                                                   .Include(x => x.ExternalMembers.Where(em => em.State == State.NotDeleted))
                                                   .FirstOrDefault();

            if (committee == null)
            {
                return _responseHelper.NotFound("CommitteeNotFound!");
            }

            committee.State = State.Deleted;
            committee.UpdatedBy = _loggedInUserId;

            _committeeRepo.Update(committee);

            // Soft delete Committee Attachments
            if (committee.Attachments.Any())
            {
                committee.Attachments.ToList().ExecuteBulkSoftDelete(_attachmentRepo);
            }

            // Soft delete Committee WorkRules
            if (committee.WorkRules.Any())
            {
                committee.WorkRules.ToList().ExecuteBulkSoftDelete(_workRuleRepo);
            }

            // Soft delete Committee Targets
            if (committee.Targets.Any())
            {
                committee.Targets.ToList().ExecuteBulkSoftDelete(_targetRepo);
            }

            // Soft delete Committee ExternalMembers
            if (committee.ExternalMembers.Any())
            {
                committee.ExternalMembers.ToList().ExecuteBulkSoftDelete(_externalMemberRepo);
            }

            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

            return _responseHelper.SavedSuccessfully("CommitteeDeletedSuccessfully!");
        }
    }
}
