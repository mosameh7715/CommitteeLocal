namespace Committees.Application.Features.OutputTypes.Query.GetAll
{
    public class AllOutputTypesDTO
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string CSSClasses { get; set; }
    }
}
