namespace Committees.Application.Features.OutputTypes.Command.Put
{
    public class PutOutputTypeHandler : IRequestHandler<PutOutputTypeCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGRepository<OutputType> _outputTypeRepository;
        private readonly ResponseDTO _responseDto;
        private readonly IResponseHelper _responseHelper;
        private readonly IMapper _mapper;
        private readonly ILogger<PutOutputTypeHandler> _logger;
        public Guid _loggedInUserId;

        public PutOutputTypeHandler
        (
            IUnitOfWork unitOfWork,
            IGRepository<OutputType> outputTypeRepository,
            IMapper mapper,
            IResponseHelper responseHelper,
            ILogger<PutOutputTypeHandler> logger,
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

        public async Task<ResponseDTO> Handle(PutOutputTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new PutOutputTypeValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                _responseDto.Result = null;
                _responseDto.StatusEnum = StatusEnum.Exception;
                _responseDto.Message = JsonSerializer.Serialize(validationResult.Errors.Select(n => n.ErrorMessage).ToList());
                return _responseDto;
            }

            var outputTypeToUpdate = await _outputTypeRepository.GetFirstAsync(x => x.Id == request.OutputTypeId);

            if (outputTypeToUpdate == null)
            {
                return _responseHelper.NotFound("OutputTypeNotFound!");
            }

            var updatedOutputType = _mapper.Map<OutputType>(request);

            updatedOutputType.Id = outputTypeToUpdate.Id;
            updatedOutputType.RowVersion = outputTypeToUpdate.RowVersion;
            updatedOutputType.UpdatedBy = _loggedInUserId;

            _outputTypeRepository.Update(updatedOutputType);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

            return _responseHelper.SavedSuccessfully("OutputTypeUpdatedSuccessfully!");
        }
    }
}
