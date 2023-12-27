namespace Committees.Application.Features.OutputTypes.Query.GetAll
{
    public class GetAllOutputTypesHandler : IRequestHandler<GetAllOutputTypesQuery, ResponseDTO>
    {
        private readonly IGRepository<OutputType> _outputTypeRepository;
        private readonly IResponseHelper _responseHelper;
        private ResponseDTO _responseDto;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllOutputTypesHandler> _logger;

        public GetAllOutputTypesHandler
        (
            IGRepository<OutputType> outputTypeRepository,
            IResponseHelper responseHelper,
            ILogger<GetAllOutputTypesHandler> logger,
            IMapper mapper
        )
        {
            _outputTypeRepository = outputTypeRepository;
            _responseDto = new ResponseDTO();
            _responseHelper = responseHelper;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> Handle(GetAllOutputTypesQuery request, CancellationToken cancellationToken)
        {
            var allOutputTypes = _outputTypeRepository.GetAll().ToList();

            if (!allOutputTypes.Any())
            {
                return _responseHelper.NotFound("OutputTypesNotFound!");
            }

            var outputTypeDto = _mapper.Map<List<AllOutputTypesDTO>>(allOutputTypes);

            return _responseHelper.RetrievedSuccessfully(outputTypeDto, "OutputTypesRetrievedSuccessfully!");
        }
    }
}
