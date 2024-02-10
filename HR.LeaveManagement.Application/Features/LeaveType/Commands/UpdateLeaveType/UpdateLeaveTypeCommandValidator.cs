using Application.Contracts.Persistence;
using Application.Features.LeaveType.Commands.CreateLeaveType;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LeaveType.Commands.UpdateLeaveType
{
	public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
	{
		private readonly ILeaveTypeRepository _repository;

		public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository repository)
		{
			RuleFor(p => p.Id)
				.NotNull()
				.MustAsync(LeaveTypeMustExist);

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

		private async Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command, CancellationToken token)
		{
			return await _repository.IsLeaveTypeUnique(command.Name);
		}

		private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
		{
			var leaveType = await _repository.GetByIdAsync(id);
			return leaveType != null;
		}
	}
}
