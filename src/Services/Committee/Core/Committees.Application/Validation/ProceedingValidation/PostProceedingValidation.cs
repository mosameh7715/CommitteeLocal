namespace Committees.Application.Validation.ProceedingValidation
{
    public class PostProceedingValidation : AbstractValidator<PostProceedingDto>
    {
        public PostProceedingValidation()
        {
            RuleFor(proceeding => proceeding.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");

            RuleFor(proceeding => proceeding.Notes)
                .NotEmpty().WithMessage("Notes is required.")
                .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters.");

            RuleFor(proceeding => proceeding.ProceedingAttachments)
                .Cascade(CascadeMode.Stop)
                .Must(files => files == null || files.All(file => IsValidFile(file)))
                .WithMessage("All proceeding attachments must be valid files.");

            RuleFor(proceeding => proceeding.MeetingId)
                .NotEmpty().WithMessage("At least one meeting must be associated with the proceeding.");
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
