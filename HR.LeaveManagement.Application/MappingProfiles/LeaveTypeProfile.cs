using Application.Features.LeaveType.Queries.GetAllLeaveType;
using Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using AutoMapper;
using HR.LeaveManagement.Domain;

namespace Application.MappingProfiles
{
	public class LeaveTypeProfile : Profile
	{
		public LeaveTypeProfile()
		{
			CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
			CreateMap<LeaveType, LeaveTypeDetailsDto>();
		}
	}
}
