namespace Committees.Application.Features.PermissionTypes.Command.Put
{
    public class PutPermissionHandler : IRequestHandler<PutPermissionCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGRepository<Permission> _permissionRepository;
        private readonly ResponseDTO _responseDto;
        private readonly ResponseHelper _responseHelper;
        private readonly IMapper _mapper;
        private readonly ILogger<PutPermissionHandler> _logger;
        public Guid _loggedInUserId;

        public PutPermissionHandler
        (
            IUnitOfWork unitOfWork,
            IGRepository<Permission> permissionRepository,
            IMapper mapper,
            IResponseHelper responseHelper,
            ILogger<PutPermissionHandler> logger,
            IHttpContextAccessor _httpContextAccessor
        )
        {
            _unitOfWork = unitOfWork;
            _permissionRepository = permissionRepository;
            _mapper = mapper;
            _responseDto = new ResponseDTO();
            _responseHelper = new ResponseHelper();
            _logger = logger;
            _loggedInUserId = Infrastructure.Helpers.LoggedInUserProvider.GetLoggedInUserId(_httpContextAccessor);
        }

        public async Task<ResponseDTO> Handle(PutPermissionCommand request, CancellationToken cancellationToken)
        {
            var validator = new PutPermissionValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                _responseDto.Result = null;
                _responseDto.StatusEnum = StatusEnum.Exception;
                _responseDto.Message = JsonSerializer.Serialize(validationResult.Errors.Select(n => n.ErrorMessage).ToList());
                return _responseDto;
            }

            var permissionToUpdate = await _permissionRepository.GetFirstAsync(x => x.Id == request.PermissionId);

            if (permissionToUpdate == null)
            {
                return _responseHelper.NotFound("PermissionNotFound!");
            }

            var updatedPermission = _mapper.Map<Permission>(request);

            updatedPermission.Id = permissionToUpdate.Id;
            updatedPermission.RowVersion = permissionToUpdate.RowVersion;
            updatedPermission.UpdatedBy = _loggedInUserId;

            _permissionRepository.Update(updatedPermission);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

            return _responseHelper.SavedSuccessfully("PermissionUpdatedSuccessfully!");
        }
    }
}
