namespace Committees.Infrastructure.ModelsConfigurations
{
	public class ExternalMemberProceedingConfig:IEntityTypeConfiguration<ExternalMemberProceeding>
	{
		public void Configure(EntityTypeBuilder<ExternalMemberProceeding> builder)
		{
			builder
				.HasKey(x => new { x.ExternalMemberId,x.ProceedingId });
		}
	}
}
