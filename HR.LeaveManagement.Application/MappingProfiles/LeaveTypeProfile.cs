using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using AutoMapper;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.MappingProfiles
{
	public class LeaveTypeProfile : Profile
	{
		public LeaveTypeProfile()
		{
			CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
			CreateMap<LeaveType, LeaveTypeDetailsDto>();
			CreateMap<CreateLeaveTypeCommand, LeaveType>();
			CreateMap<UpdateLeaveTypeCommand, LeaveType>();
		}
	}
}
