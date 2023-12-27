namespace Committees.Application.Features.OutputTypes.Command.Post
{
    public class PostOutputTypeCommand : IRequest<ResponseDTO>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string CSSClasses { get; set; }
    }
}
