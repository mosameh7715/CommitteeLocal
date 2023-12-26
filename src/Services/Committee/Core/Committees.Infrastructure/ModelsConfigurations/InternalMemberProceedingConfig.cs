namespace Committees.Infrastructure.ModelsConfigurations
{
	public class InternalMemberProceedingConfig:IEntityTypeConfiguration<InternalMemberProceeding>
	{
		public void Configure(EntityTypeBuilder<InternalMemberProceeding> builder)
		{
			builder
				.HasKey(x => new { x.InternalMemberId,x.ProceedingId });
		}
	}
}
