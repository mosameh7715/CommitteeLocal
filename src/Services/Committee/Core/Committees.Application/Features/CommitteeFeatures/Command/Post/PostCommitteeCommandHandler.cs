namespace Committees.Application.Features.CommitteeFeatures.Command.Post
{
    public class PostCommitteeCommandHandler : IRequestHandler<PostCommitteeCommand, ResponseDTO>
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
        private readonly IWebHostEnvironment _hosting;
        public Guid _loggedInUserId;
        public PostCommitteeCommandHandler
        (
           IGRepository<CommitteeAttachment> attachmentRepo,
           IGRepository<ExternalMember> externalMemberRepo,
           IHttpContextAccessor _httpContextAccessor,
           IGRepository<Committee> committeeRepo,
           IGRepository<WorkRule> workRuleRepo,
           IGRepository<Target> targetRepo,
           IResponseHelper responseHelper,
           IWebHostEnvironment hosting,
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
            _hosting = hosting;
            _mapper = mapper;
            _loggedInUserId = Infrastructure.Helpers.LoggedInUserProvider.GetLoggedInUserId(_httpContextAccessor);
        }
        public async Task<ResponseDTO> Handle(PostCommitteeCommand request, CancellationToken cancellationToken)
        {
            var validator = new PostCommitteeValidation();
            var validation = validator.Validate(request.CommitteeDto);

            if (!validation.IsValid)
            {
                _responseDTO.Result = null;
                _responseDTO.StatusEnum = StatusEnum.Exception;
                _responseDTO.Message = string.Join(", ", validation.Errors.Select(n => n.ErrorMessage));
                return _responseDTO;
            }

            var newCommittee = _mapper.Map<Committee>(request.CommitteeDto);
            _committeeRepo.Add(newCommittee);

            if (request.CommitteeDto.HasLegalDocument)
            {
                newCommittee.LegalDocument = request.CommitteeDto.LegalDocument;
            }

            // Adding Committee Attachments
            if (request.CommitteeDto.Attachments != null)
            {
                var attachments = await Task.WhenAll(
                    request.CommitteeDto.Attachments.Select(async att =>
                    {
                        var file = await Upload.UploadFiles(att, _hosting, newCommittee.Id.ToString() + "CommitteeAttachments");
                        return new CommitteeAttachment
                        {
                            CommitteeId = newCommittee.Id,
                            Path = file,
                            CreatedBy = _loggedInUserId
                        };
                    })
                );
                _attachmentRepo.AddRange(attachments);
            }

            // Adding Committee WorkRules Attachments
            if (request.CommitteeDto.WorkRules != null)
            {
                var workRules = await Task.WhenAll(
                    request.CommitteeDto.WorkRules.Select(async rule =>
                    {
                        var file = await Upload.UploadFiles(rule, _hosting, newCommittee.Id.ToString() + "CommitteeWorkRules");
                        return new WorkRule
                        {
                            CommitteeId = newCommittee.Id,
                            Path = file,
                            CreatedBy = _loggedInUserId
                        };
                    })
                );
                _workRuleRepo.AddRange(workRules);
            }

            // Add Targets
            if (request.CommitteeDto.Targets != null)
            {
                var targets = request.CommitteeDto.Targets.Select(targetDto =>
                {
                    var target = _mapper.Map<Target>(targetDto);
                    target.CommitteeId = newCommittee.Id;
                    return target;
                }).ToList();

                _targetRepo.AddRange(targets);
            }

            // Add ExternalMembers
            if (request.CommitteeDto.ExternalMembers != null)
            {
                var externalMembers = request.CommitteeDto.ExternalMembers.Select(externalMemberDto =>
                {
                    var externalMember = _mapper.Map<ExternalMember>(externalMemberDto);
                    externalMember.CommitteeId = newCommittee.Id;
                    return externalMember;
                }).ToList();

                _externalMemberRepo.AddRange(externalMembers);
            }

            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

            return _responseHelper.SavedSuccessfully("CommitteeAddedSuccessfully!");
        }
    }
}
