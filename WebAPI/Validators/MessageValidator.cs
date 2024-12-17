using BLL.DTOs;
using DAL.Interfaces;
using FluentValidation;

namespace WebAPI.Validators;

public class MessageValidator : AbstractValidator<CreateMessageDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public MessageValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(m => m.Content)
            .NotEmpty().WithMessage("Message content is required")
            .MaximumLength(4000).WithMessage("Message content cannot exceed 4000 characters");

        RuleFor(m => m.RecipientUsername)
            .NotEmpty().WithMessage("Recipient username is required")
            .MustAsync(async (username, cancellation) =>
            {
                var userExists = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
                return userExists != null;
            }).WithMessage("Recipient not found");
    }
}