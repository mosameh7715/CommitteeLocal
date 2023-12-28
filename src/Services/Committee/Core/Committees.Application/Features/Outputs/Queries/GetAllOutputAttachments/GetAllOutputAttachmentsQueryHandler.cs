namespace Committees.Application.Features.Outputs.Queries.GetAllOutputAttachments
{
	public class GetAllOutputAttachmentsQueryHandler:IRequestHandler<GetAllOutputAttachmentsQuery,ResponseDTO>
	{
		private readonly IGRepository<OutputAttachment> _attachmentRepo;
		private readonly IGRepository<Output> _outputRepo;
		private readonly IResponseHelper _responseHelper;
		private readonly IMapper _mapper;

		public GetAllOutputAttachmentsQueryHandler(IResponseHelper responseHelper,
										  IGRepository<OutputAttachment> attachmentRepo,
										  IMapper mapper,
										  IGRepository<Output> outputRepo)
		{
			_attachmentRepo = attachmentRepo;
			_responseHelper = responseHelper;
			_mapper = mapper;
			_outputRepo = outputRepo;
		}
		public async Task<ResponseDTO> Handle(GetAllOutputAttachmentsQuery request,CancellationToken cancellationToken)
		{
			var output = _outputRepo.GetAll(x => x.Id == request.OutputId).FirstOrDefault();

			if(output == null)
			{
				return _responseHelper.NotFound("outputIsNotFound");
			}

			var attachment = _attachmentRepo.GetAll(x => x.OutputId == request.OutputId).ToList();

			var attachmentsMapped = _mapper.Map<List<OutputAttachmentDto>>(attachment);


			return _responseHelper.RetrievedSuccessfully(attachmentsMapped,"outputAttachmentsIsRetrievedSuccessfully");
		}
	}
}
