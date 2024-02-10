using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.Features.LeaveType.Commands.CreateLeaveType
{
	public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
	{
		private readonly ILeaveTypeRepository _repository;

		public CreateLeaveTypeCommandValidator(ILeaveTypeRepository repository)
		{
			RuleFor(p => p.Name)
				.NotEmpty().WithMessage("{PropertyName} is required")
				.NotNull()
				.MaximumLength(50).WithMessage("{PropertyName} must be fewer than 50 characters");

			RuleFor(p => p.DefaultDays)
				.LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
				.GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");

			RuleFor(q => q)
				.MustAsync(LeaveTypeNameUnique).WithMessage("Leave Type already exists");

			_repository = repository;
		}

		private async Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
		{
			return await _repository.IsLeaveTypeUnique(command.Name);
		}
	}
}
