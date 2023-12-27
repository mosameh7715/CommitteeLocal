namespace Committees.Application.Features.PermissionTypes.Command.Post
{
    public class PostPermissionHandler : IRequestHandler<PostPermissionCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGRepository<Permission> _permissionRepository;
        private readonly IMapper _mapper;
        private readonly ResponseDTO _responseDto;
        private readonly IResponseHelper _responseHelper;
        private readonly ILogger<PostPermissionHandler> _logger;

        public PostPermissionHandler
        (
            IUnitOfWork unitOfWork,
            IGRepository<Permission> permissionRepository,
            IMapper mapper,
            IResponseHelper responseHelper,
            ILogger<PostPermissionHandler> logger
        )
        {
            _unitOfWork = unitOfWork;
            _permissionRepository = permissionRepository;
            _mapper = mapper;
            _responseDto = new ResponseDTO();
            _responseHelper = responseHelper;
            _logger = logger;
        }

        public async Task<ResponseDTO> Handle(PostPermissionCommand request, CancellationToken cancellationToken)
        {
            var validator = new PostPermissionValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                _responseDto.Result = null;
                _responseDto.StatusEnum = StatusEnum.Exception;
                _responseDto.Message = JsonSerializer.Serialize(validationResult.Errors.Select(n => n.ErrorMessage).ToList());
                return _responseDto;
            }

            var permissionToAdd = _mapper.Map<Permission>(request);
            _permissionRepository.Add(permissionToAdd);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

            return _responseHelper.SavedSuccessfully(permissionToAdd.Id, "PermissionAddedSuccessfully!");
        }
    }
}
