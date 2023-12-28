namespace Committees.Infrastructure.ModelsConfigurations
{
	public class ProceedingConfig:IEntityTypeConfiguration<Proceeding>
	{
		public void Configure(EntityTypeBuilder<Proceeding> builder)
		{
			builder.HasMany(a => a.ProceedingAttachments)
					.WithOne(a => a.Proceeding)
					.HasForeignKey(a => a.ProceedingId);

			builder.HasMany(a => a.InternalMemberProceedings)
					.WithOne(a => a.Proceeding)
					.HasForeignKey(a => a.ProceedingId);
		}
	}
}
