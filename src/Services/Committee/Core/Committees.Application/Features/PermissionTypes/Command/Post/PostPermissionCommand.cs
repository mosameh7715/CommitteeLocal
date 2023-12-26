namespace Committees.Application.Features.PermissionTypes.Command.Post
{
    public class PostPermissionCommand : IRequest<ResponseDTO>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string CSSClasses { get; set; }
    }
}
