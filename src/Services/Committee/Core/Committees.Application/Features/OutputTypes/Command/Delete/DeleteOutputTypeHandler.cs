namespace Committees.Application.Features.OutputTypes.Command.Delete
{
    public class DeleteOutputTypeHandler : IRequestHandler<DeleteOutputTypeCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGRepository<OutputType> _outputTypeRepository;
        private readonly ResponseDTO _responseDto;
        private readonly IResponseHelper _responseHelper;
        private readonly ILogger<DeleteOutputTypeHandler> _logger;

        public DeleteOutputTypeHandler
        (
            IUnitOfWork unitOfWork,
            IGRepository<OutputType> outputTypeRepository,
            IResponseHelper responseHelper,
            ILogger<DeleteOutputTypeHandler> logger
        )
        {
            _unitOfWork = unitOfWork;
            _outputTypeRepository = outputTypeRepository;
            _responseDto = new ResponseDTO();
            _responseHelper = responseHelper;
            _logger = logger;
        }

        public async Task<ResponseDTO> Handle(DeleteOutputTypeCommand request, CancellationToken cancellationToken)
        {
            var outputTypeToDelete = _outputTypeRepository.GetAll(x => x.Id == request.OutputTypeId).FirstOrDefault();

            if (outputTypeToDelete == null)
            {
                return _responseHelper.NotFound("OutputTypeNotFound!");
            }

            outputTypeToDelete.State = State.Deleted;
            _outputTypeRepository.Update(outputTypeToDelete);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

            return _responseHelper.SavedSuccessfully("OutputTypeDeletedSuccessfully!");
        }
    }
}
