using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveType.Queries.GetAllLeaveType
{
	public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
	{
		private readonly ILeaveTypeRepository _repository;
		private readonly IMapper _mapper;

		public GetLeaveTypesQueryHandler(ILeaveTypeRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
		{
			var leaveTypes = await _repository.GetAsync();
			var dtos = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);
			return dtos;
		}
	}
}
