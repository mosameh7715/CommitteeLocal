namespace Committees.Application.Features.CommitteeFeatures.Command.Put
{
    public class PutCommitteeCommandHandler : IRequestHandler<PutCommitteeCommand, ResponseDTO>
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
        public PutCommitteeCommandHandler
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

        public async Task<ResponseDTO> Handle(PutCommitteeCommand request, CancellationToken cancellationToken)
        {
            var validator = new PutCommitteeValidation();
            var validation = validator.Validate(request.CommitteeDto);

            if (!validation.IsValid)
            {
                _responseDTO.Result = null;
                _responseDTO.StatusEnum = StatusEnum.Exception;
                _responseDTO.Message = string.Join(", ", validation.Errors.Select(n => n.ErrorMessage));
                return _responseDTO;
            }

            var committeeToUpdate = await _committeeRepo.GetFirstAsync(x => x.Id == request.CommitteeId);

            if (committeeToUpdate == null)
            {
                return _responseHelper.NotFound("CommitteeNotFound!");
            }

            var mappedCommittee = _mapper.Map(request.CommitteeDto, committeeToUpdate);
            mappedCommittee.Id = request.CommitteeId;
            mappedCommittee.CreatedBy = committeeToUpdate.CreatedBy;
            mappedCommittee.CreatedOn = committeeToUpdate.CreatedOn;
            mappedCommittee.RowVersion = committeeToUpdate.RowVersion;
            mappedCommittee.UpdatedBy = _loggedInUserId;

            _committeeRepo.Update(mappedCommittee);

            if (request.CommitteeDto.Attachments != null)
            {
                var attachments = await Task.WhenAll(
                    request.CommitteeDto.Attachments.Select(async att =>
                    {
                        var file = await Upload.UploadFiles(att, _hosting, committeeToUpdate.Id.ToString() + "CommitteeAttachments");
                        return new CommitteeAttachment
                        {
                            CommitteeId = committeeToUpdate.Id,
                            Path = file,
                            CreatedBy = _loggedInUserId
                        };
                    })
                );
                _attachmentRepo.AddRange(attachments);
            }

            if (request.CommitteeDto.WorkRules != null)
            {
                var workRules = await Task.WhenAll(
                    request.CommitteeDto.WorkRules.Select(async rule =>
                    {
                        var file = await Upload.UploadFiles(rule, _hosting, committeeToUpdate.Id.ToString() + "CommitteeWorkRules");
                        return new WorkRule
                        {
                            CommitteeId = committeeToUpdate.Id,
                            Path = file,
                            CreatedBy = _loggedInUserId
                        };
                    })
                );
                _workRuleRepo.AddRange(workRules);
            }

            if (request.CommitteeDto.Targets != null)
            {
                var targets = request.CommitteeDto.Targets.Select(targetDto =>
                {
                    var target = _mapper.Map<Target>(targetDto);
                    target.CommitteeId = committeeToUpdate.Id;
                    return target;
                }).ToList();

                _targetRepo.AddRange(targets);
            }

            if (request.CommitteeDto.ExternalMembers != null)
            {
                var externalMembers = request.CommitteeDto.ExternalMembers.Select(externalMemberDto =>
                {
                    var externalMember = _mapper.Map<ExternalMember>(externalMemberDto);
                    externalMember.CommitteeId = committeeToUpdate.Id;
                    return externalMember;
                }).ToList();

                _externalMemberRepo.AddRange(externalMembers);
            }

            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

            return _responseHelper.SavedSuccessfully("CommitteeUpdatedSuccessfully!");
        }
    }
}
