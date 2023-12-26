namespace Committees.Application.Features.PermissionTypes.Command.Delete
{
    public class DeletePermissionHandler : IRequestHandler<DeletePermissionCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGRepository<Permission> _permissionRepository;
        private readonly ResponseDTO _responseDto;
        private readonly ResponseHelper _responseHelper;
        private readonly ILogger<DeletePermissionHandler> _logger;

        public DeletePermissionHandler
        (
            IUnitOfWork unitOfWork,
            IGRepository<Permission> permissionRepository,
            IResponseHelper responseHelper,
            ILogger<DeletePermissionHandler> logger
        )
        {
            _unitOfWork = unitOfWork;
            _permissionRepository = permissionRepository;
            _responseDto = new ResponseDTO();
            _responseHelper = new ResponseHelper();
            _logger = logger;
        }

        public async Task<ResponseDTO> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
        {
            var permissionToDelete = _permissionRepository.GetAll(x => x.Id == request.PermissionId).FirstOrDefault();

            if (permissionToDelete == null)
            {
                return _responseHelper.NotFound("PermissionNotFound!");
            }

            permissionToDelete.State = State.Deleted;
            _permissionRepository.Update(permissionToDelete);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

            return _responseHelper.SavedSuccessfully("PermissionDeletedSuccessfully!");
        }
    }
}
