namespace Committees.Application.Features.OutputTypes.Command.Put
{
    public class PutOutputTypeCommand : IRequest<ResponseDTO>
    {
        [JsonIgnore]
        public Guid OutputTypeId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string CSSClasses { get; set; }
    }
}
