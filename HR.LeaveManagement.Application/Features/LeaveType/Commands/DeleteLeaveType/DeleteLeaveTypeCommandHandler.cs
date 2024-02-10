using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using HR.LeaveManagement.Domain;
using MediatR;

namespace Application.Features.LeaveType.Commands.DeleteLeaveType
{
	public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
	{
		private readonly ILeaveTypeRepository _repository;

		public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository repository)
		{
			_repository = repository;
		}

		public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
		{
			var leaveTypeToDelete = await _repository.GetByIdAsync(request.Id);
			if (leaveTypeToDelete == null)
			{
				throw new NotFoundException(nameof(HR.LeaveManagement.Domain.LeaveType), request.Id);
			}

			await _repository.DeleteAsync(leaveTypeToDelete);
			return Unit.Value;
		}
	}
}
