namespace Committees.Application.Features.PermissionTypes.Command.Put
{
    public class PutPermissionCommand : IRequest<ResponseDTO>
    {
        [JsonIgnore]
        public Guid PermissionId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string CSSClasses { get; set; }
    }
}
