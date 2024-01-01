namespace Committees.Application.Features.ProceedingFeatures.Command.Post
{
    public class PostProceedingCommandHandler : IRequestHandler<PostProceedingCommand, ResponseDTO>
    {
        private readonly IGRepository<Proceeding> _proceedingRepo;
        private readonly IGRepository<ProceedingAttachment> _proceedingAttachmentRepo;
        private readonly IMapper _mapper;
        private readonly IResponseHelper _responseHelper;
        private readonly ResponseDTO _responseDto;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hosting;
        public Guid _loggedInUserId;

        public PostProceedingCommandHandler
        (
            IGRepository<Proceeding> proceedingRepo,
            IGRepository<ProceedingAttachment> proceedingAttachmentRepo,
            IHttpContextAccessor _httpContextAccessor,
            IMapper mapper,
            IResponseHelper responseHelper,
            IUnitOfWork unitOfWork,
            IWebHostEnvironment hosting
        )
        {
            _proceedingRepo = proceedingRepo;
            _proceedingAttachmentRepo = proceedingAttachmentRepo;
            _mapper = mapper;
            _responseHelper = responseHelper;
            _responseDto = new ResponseDTO();
            _unitOfWork = unitOfWork;
            _hosting = hosting;
            _loggedInUserId = Infrastructure.Helpers.LoggedInUserProvider.GetLoggedInUserId(_httpContextAccessor);
        }

        public async Task<ResponseDTO> Handle(PostProceedingCommand request, CancellationToken cancellationToken)
        {
            var validator = new PostProceedingValidation();
            var validation = validator.Validate(request.ProceedingDto);

            if (!validation.IsValid)
            {
                _responseDto.Result = null;
                _responseDto.StatusEnum = StatusEnum.Exception;
                _responseDto.Message = JsonSerializer.Serialize(validation.Errors.Select(n => n.ErrorMessage).ToList());
                return _responseDto;
            }

            var proceedingToAdd = _mapper.Map<Proceeding>(request.ProceedingDto);

            _proceedingRepo.Add(proceedingToAdd);

            if (request.ProceedingDto.ProceedingAttachments != null)
            {
                var proceedingAttachments = await Task.WhenAll(
                    request.ProceedingDto.ProceedingAttachments.Select(async att =>
                    {
                        var file = await Upload.UploadFiles(att, _hosting, proceedingToAdd.Id.ToString() + "ProceedingAttachments");
                        return new ProceedingAttachment
                        {
                            ProceedingId = proceedingToAdd.Id,
                            Path = file,
                            CreatedBy = _loggedInUserId
                        };
                    })
                );
                _proceedingAttachmentRepo.AddRange(proceedingAttachments);
            }

            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

            return _responseHelper.SavedSuccessfully("ProceedingAddedSuccessfully!");
        }
    }
}
