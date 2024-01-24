using API.Resources;
using FluentValidation;

namespace API.Validators
{
    public class SaveMeetingResourceValidator : AbstractValidator<SaveMeetingResource>
    {
        public SaveMeetingResourceValidator()
        {
            RuleFor(m => m.MeetingId)
                .NotEmpty()
                .WithMessage("Meeting ID must not be 0.");

            RuleFor(m => m.RoomId)
                .NotEmpty()
                .WithMessage("Room ID must not be 0.");
        }
    }
}
