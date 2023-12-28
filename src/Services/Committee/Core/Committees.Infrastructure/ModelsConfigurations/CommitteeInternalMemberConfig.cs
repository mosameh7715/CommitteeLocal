namespace Committees.Infrastructure.ModelsConfigurations
{
	public class CommitteeInternalMemberConfig:IEntityTypeConfiguration<CommitteeInternalMember>
	{
		public void Configure(EntityTypeBuilder<CommitteeInternalMember> builder)
		{
			builder
				.HasKey(x => new { x.InternalMemberId,x.CommitteeId });
		}
	}
}
