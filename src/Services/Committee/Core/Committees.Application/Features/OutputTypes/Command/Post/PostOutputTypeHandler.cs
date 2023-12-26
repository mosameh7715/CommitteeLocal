namespace Committees.Application.Features.OutputTypes.Command.Post
{
    public class PostOutputTypeHandler : IRequestHandler<PostOutputTypeCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGRepository<OutputType> _outputTypeRepository;
        private readonly IMapper _mapper;
        private readonly ResponseDTO _responseDto;
        private readonly IResponseHelper _responseHelper;
        private readonly ILogger<PostOutputTypeHandler> _logger;
        public Guid _loggedInUserId;

        public PostOutputTypeHandler
        (
            IUnitOfWork unitOfWork,
            IGRepository<OutputType> outputTypeRepository,
            IMapper mapper,
            IResponseHelper responseHelper,
            ILogger<PostOutputTypeHandler> logger,
            IHttpContextAccessor _httpContextAccessor
        )
        {
            _unitOfWork = unitOfWork;
            _outputTypeRepository = outputTypeRepository;
            _mapper = mapper;
            _responseDto = new ResponseDTO();
            _responseHelper = responseHelper;
            _logger = logger;
            _loggedInUserId = Infrastructure.Helpers.LoggedInUserProvider.GetLoggedInUserId(_httpContextAccessor);
        }

        public async Task<ResponseDTO> Handle(PostOutputTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new PostOutputTypeValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                _responseDto.Result = null;
                _responseDto.StatusEnum = StatusEnum.Exception;
                _responseDto.Message = JsonSerializer.Serialize(validationResult.Errors.Select(n => n.ErrorMessage).ToList());
                return _responseDto;
            }

            var outputTypeToAdd = _mapper.Map<OutputType>(request);
            outputTypeToAdd.CreatedBy = _loggedInUserId;
            _outputTypeRepository.Add(outputTypeToAdd);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

            return _responseHelper.SavedSuccessfully(outputTypeToAdd.Id, "OutputTypeAddedSuccessfully!");
        }
    }
}
