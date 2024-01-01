namespace Committees.Application.Features.OutputFeatures.Command.Post
{
    public class PostOutputCommandHandler : IRequestHandler<PostOutputCommand, ResponseDTO>
    {
        private readonly IGRepository<Output> _outputRepo;
        private readonly IGRepository<OutputAttachment> _outputAttachmentRepo;
        private readonly IMapper _mapper;
        private readonly ResponseDTO _responseDto;
        private readonly IResponseHelper _responseHelper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hosting;
        public Guid _loggedInUserId;

        public PostOutputCommandHandler
        (
            IGRepository<Output> outputRepo,
            IGRepository<OutputAttachment> outputAttachmentRepo,
            IHttpContextAccessor _httpContextAccessor,
            IMapper mapper,
            IResponseHelper responseHelper,
            IUnitOfWork unitOfWork,
            IWebHostEnvironment hosting
        )
        {
            _outputRepo = outputRepo;
            _outputAttachmentRepo = outputAttachmentRepo;
            _mapper = mapper;
            _responseHelper = responseHelper;
            _responseDto = new ResponseDTO();
            _unitOfWork = unitOfWork;
            _hosting = hosting;
            _loggedInUserId = Infrastructure.Helpers.LoggedInUserProvider.GetLoggedInUserId(_httpContextAccessor);
        }

        public async Task<ResponseDTO> Handle(PostOutputCommand request, CancellationToken cancellationToken)
        {
            var validator = new PostOutputValidation();
            var validation = validator.Validate(request.OutputDto);

            if (!validation.IsValid)
            {
                _responseDto.Result = null;
                _responseDto.StatusEnum = StatusEnum.Exception;
                _responseDto.Message = JsonSerializer.Serialize(validation.Errors.Select(n => n.ErrorMessage).ToList());
                return _responseDto;
            }

            var outputToAdd = _mapper.Map<Output>(request.OutputDto);
            outputToAdd.CreatedBy = _loggedInUserId;

            _outputRepo.Add(outputToAdd);

            if (request.OutputDto.OutputAttachments != null)
            {
                var outputAttachments = await Task.WhenAll(
                    request.OutputDto.OutputAttachments.Select(async att =>
                    {
                        var file = await Upload.UploadFiles(att, _hosting, outputToAdd.Id.ToString() + "OutputAttachments");
                        return new OutputAttachment
                        {
                            OutputId = outputToAdd.Id,
                            Path = file,
                            CreatedBy = _loggedInUserId
                        };
                    })
                );
                _outputAttachmentRepo.AddRange(outputAttachments);
            }

            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

            return _responseHelper.SavedSuccessfully("OutputAddedSuccessfully!");
        }
    }
}
