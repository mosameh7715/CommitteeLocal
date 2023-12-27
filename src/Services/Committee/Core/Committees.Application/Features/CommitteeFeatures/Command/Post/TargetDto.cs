namespace Committees.Application.Features.CommitteeFeatures.Command.Post
{
    public class TargetDto
    {
        public string Goal { get; set; }
        [JsonIgnore]
        public Guid CommitteeId { get; set; }
    }
}
