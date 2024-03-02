using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("login")]
		public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
		{
			var result = await _authService.Login(request);
			return Ok(result);
		}

		[HttpPost("register")]
		public async Task<ActionResult<AuthResponse>> Register(RegistrationRequest request)
		{
			var result = await _authService.Register(request);
			return Ok(result);
		}
	}
}
