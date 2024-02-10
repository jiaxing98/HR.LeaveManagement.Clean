using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using AutoMapper;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
	public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
	{
		private readonly ILeaveTypeRepository _repository;
		private readonly IMapper _mapper;

		public CreateLeaveTypeCommandHandler(ILeaveTypeRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateLeaveTypeCommandValidator(_repository);
			var validationResult = await validator.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				throw new BadRequestException("Invalid Leave Type", validationResult);
			}
				
			var leaveTypeToCreate = _mapper.Map<HR.LeaveManagement.Domain.LeaveType>(request);
			await _repository.CreateAsync(leaveTypeToCreate);
			return leaveTypeToCreate.Id;
		}
	}
}
