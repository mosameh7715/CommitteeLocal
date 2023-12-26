namespace Committees.Infrastructure.ModelsConfigurations
{
	public class OutputConfig:IEntityTypeConfiguration<Output>
	{
		public void Configure(EntityTypeBuilder<Output> builder)
		{
			builder.HasMany(a => a.OutputAttachments)
					.WithOne(a => a.Output)
					.HasForeignKey(a => a.OutputId);
		}
	}
}
