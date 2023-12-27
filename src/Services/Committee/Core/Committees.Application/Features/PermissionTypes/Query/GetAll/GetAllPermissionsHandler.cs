namespace Committees.Application.Features.PermissionTypes.Query.GetAll
{
    public class GetAllPermissionsHandler : IRequestHandler<GetAllPermissionsQuery, ResponseDTO>
    {
        private readonly IGRepository<Permission> _permissionRepository;
        private readonly IResponseHelper _responseHelper;
        private ResponseDTO _responseDto;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPermissionsHandler> _logger;

        public GetAllPermissionsHandler
        (
            IGRepository<Permission> permissionRepository,
            IResponseHelper responseHelper,
            ILogger<GetAllPermissionsHandler> logger,
            IMapper mapper
        )
        {
            _permissionRepository = permissionRepository;
            _responseDto = new ResponseDTO();
            _responseHelper = responseHelper;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            var allPermissions = _permissionRepository.GetAll().ToList();

            if (!allPermissions.Any())
            {
                return _responseHelper.NotFound("PermissionsNotFound!");
            }

            var permissionDto = _mapper.Map<List<AllPermissionsDTO>>(allPermissions);

            return _responseHelper.RetrievedSuccessfully(permissionDto, "PermissionsRetrievedSuccessfully!");
        }
    }
}
