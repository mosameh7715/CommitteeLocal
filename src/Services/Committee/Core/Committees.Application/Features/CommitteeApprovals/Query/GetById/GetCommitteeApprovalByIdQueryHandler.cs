//namespace Committees.Application.Features.CommitteeApprovals.Query.GetById
//{
//	public class GetCommitteeApprovalByIdQueryHandler:IRequestHandler<GetCommitteeApprovalByIdQuery,ResponseDTO>
//	{
//		private readonly IGRepository<SubKnowledge> _subKnowledgeRepo;
//		private readonly IGRepository<KnowledgeViews> _viewsRepo;
//		private readonly IUnitOfWork _unitOfWork;
//		private readonly IResponseHelper _responseHelper;
//		private readonly IMapper _mapper;
//		public Guid _loggedInUserId;

//		public GetCommitteeApprovalByIdQueryHandler(IResponseHelper responseHelper,
//										  IGRepository<SubKnowledge> subKnowledgeRepo,
//										  IMapper mapper,
//										  IHttpContextAccessor _httpContextAccessor,
//										  IGRepository<KnowledgeViews> viewsRepo,
//										  IUnitOfWork unitOfWork)
//		{
//			_subKnowledgeRepo = subKnowledgeRepo;
//			_responseHelper = responseHelper;
//			_mapper = mapper;
//			_loggedInUserId = LoggedInUserProvider.GetLoggedInUserId(_httpContextAccessor);
//			_viewsRepo = viewsRepo;
//			_unitOfWork = unitOfWork;
//		}
//		public async Task<ResponseDTO> Handle(GetCommitteeApprovalByIdQuery request,CancellationToken cancellationToken)
//		{
//			var subKnowledge = _subKnowledgeRepo.GetAll(x => x.Id == request.SubKnowledgeId)
//											  .Include(x => x.Knowledge)
//											  .Include(x => x.Category)
//											  .Include(x => x.KnowledgeFavoirates.Where(c => c.State == State.NotDeleted))
//											  .Include(x => x.KnowledgeLikes.Where(a => a.State == State.NotDeleted))
//											  .Include(x => x.KnowledgeRates.Where(a => a.State == State.NotDeleted))
//											  .Include(x => x.KnowledgeViews.Where(a => a.State == State.NotDeleted))
//											  .Include(x => x.ParticipatingParties.Where(a => a.State == State.NotDeleted))
//											  .Include(x => x.WorkParticipants.Where(a => a.State == State.NotDeleted))
//											  .Include(x => x.KnowledgeAttachments.Where(a => a.State == State.NotDeleted))
//											  .OrderByDescending(x => x.CreatedOn)
//											  .FirstOrDefault();

//			if(subKnowledge == null)
//			{
//				return _responseHelper.NotFound("subKnowledgeIsNotExists");
//			}

//			//POST A VIEW FOR THE CURRENT USER
//			var isUserViewed = _viewsRepo.GetAll().Any(x => x.CreatedBy == _loggedInUserId && x.SubKnowledgeId == subKnowledge.Id);

//			if(!isUserViewed)
//			{
//				var newView = new KnowledgeViews
//				{
//					SubKnowledgeId = subKnowledge.Id,
//				};
//				newView.CreatedBy = _loggedInUserId;
//				_viewsRepo.Add(newView);

//				_unitOfWork.SaveChanges();
//				_unitOfWork.Commit();
//			}

//			var subKnowledgeMapped = _mapper.Map<CommitteeApprovalByIdDto>(subKnowledge);

//			subKnowledgeMapped.IsFavoirate = subKnowledge.KnowledgeFavoirates.Any(favorite => favorite.CreatedBy == _loggedInUserId && 
//																						favorite.SubKnowledgeId == subKnowledgeMapped.Id);

//			return _responseHelper.RetrievedSuccessfully(subKnowledgeMapped,"subKnowledgeIsRetrievedSuccessfully");
//		}
//	}
//}
