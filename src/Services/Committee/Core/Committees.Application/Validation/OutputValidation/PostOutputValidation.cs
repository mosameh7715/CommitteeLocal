namespace Committees.Application.Validation.OutputValidation
{
    public class PostOutputValidation : AbstractValidator<PostOutputDto>
    {
        public PostOutputValidation()
        {
            RuleFor(output => output.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");

            RuleFor(output => output.Details)
                .NotEmpty().WithMessage("Details is required.")
                .MaximumLength(1000).WithMessage("Details cannot exceed 1000 characters.");

            RuleFor(output => output.OutputAttachments)
                .Cascade(CascadeMode.Stop)
                .Must(files => files == null || files.All(file => IsValidFile(file)))
                .WithMessage("All output attachments must be valid files.");

            RuleFor(output => output.MeetingIds)
                .NotEmpty().WithMessage("At least one meeting must be associated with the output.");
        }

        private bool IsValidFile(IFormFile file)
        {
            if (file == null)
                return false;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".doc", ".docx", ".mp4", ".avi", ".mov", ".wmv" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(fileExtension);
        }
    }

}
