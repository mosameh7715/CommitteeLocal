using Committees.Application.Features.MeetingsFeatures.Command.Post;

namespace Committees.Application.Validation.MeetingsValidations
{
    public class PostMeetingValidation : AbstractValidator<PostMeetingDto>
    {
        public PostMeetingValidation()
        {
            RuleFor(meeting => meeting.Rules)
                .NotEmpty().WithMessage("Rules is required.")
                .MaximumLength(1000).WithMessage("Rules cannot exceed 1000 characters.");

            RuleFor(meeting => meeting.Location)
                .NotEmpty().WithMessage("Location is required.")
                .MaximumLength(255).WithMessage("Location cannot exceed 255 characters.");

            RuleFor(meeting => meeting.MeetingAttachments)
                .Cascade(CascadeMode.Stop)
                .Must(files => files == null || files.All(file => IsValidFile(file)))
                .WithMessage("All meeting attachments must be valid files.");
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
