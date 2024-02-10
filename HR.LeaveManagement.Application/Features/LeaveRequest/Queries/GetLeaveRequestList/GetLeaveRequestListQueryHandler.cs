using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList
{
    public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
    {
        private readonly IMapper _mapper;

        private readonly ILeaveRequestRepository _repository;

        public GetLeaveRequestListQueryHandler(IMapper mapper, ILeaveRequestRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
        {

            // Check if it is logged in employee

            var leaveRequests = await _repository.GetLeaveRequestsWithDetails();
            var requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);


            // Fill requests with employee information
            return requests;
        }
    }
}
