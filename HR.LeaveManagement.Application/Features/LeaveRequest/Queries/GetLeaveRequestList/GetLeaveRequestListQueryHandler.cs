using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList
{
    public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
    {
        private readonly IMapper _mapper;

        private readonly ILeaveRequestRepository _repository;
        private readonly IUserService _userService;

        public GetLeaveRequestListQueryHandler(IMapper mapper, ILeaveRequestRepository repository, IUserService userService)
        {
            _mapper = mapper;
            _repository = repository;
            _userService = userService;
        }

        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
        {
            var leaveRequests = await _repository.GetLeaveRequestsWithDetails();
            var requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

            // Check if it is logged in employee
            if (request.IsLoggedInUser)
            {
                var userId = _userService.UserId;
                leaveRequests = await _repository.GetLeaveRequestsWithDetails(userId);

                var employee = await _userService.GetEmployee(userId);
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                foreach (var req in requests)
                {
                    req.Employee = employee;
                }
            }
            else
            {
                leaveRequests = await _repository.GetLeaveRequestsWithDetails();
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                foreach (var req in requests)
                {
                    req.Employee = await _userService.GetEmployee(req.RequestingEmployeeId);
                }
            }

            return requests;
        }
    }
}
