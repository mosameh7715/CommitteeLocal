namespace Committees.Application.Features.PermissionTypes.Query.GetAll
{
    public class AllPermissionsDTO
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string CSSClasses { get; set; }
    }
}
