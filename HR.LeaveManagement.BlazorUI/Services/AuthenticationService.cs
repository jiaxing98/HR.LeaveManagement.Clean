using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Providers;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagement.BlazorUI.Services
{
	public class AuthenticationService : BaseHttpService, IAuthenticationService
	{
		private readonly AuthenticationStateProvider _stateProvider;

		public AuthenticationService(
			IClient client,
			ILocalStorageService localStorage,
			AuthenticationStateProvider stateProvider)
			: base(client, localStorage)
		{
			_stateProvider = stateProvider;
		}

		public async Task<bool> AuthenticateAsync(string email, string password)
		{
			try
			{
				var authRequest = new AuthRequest() { Email = email, Password = password };
				var authResponse = await _client.LoginAsync(authRequest);
				if (authResponse.Token == string.Empty) return false;

				await _localStorage.SetItemAsync("token", authResponse.Token);
				await ((ApiAuthenticationStateProvider)_stateProvider).LoggedIn();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task Logout()
		{
			//await _localStorage.RemoveItemAsync("token");
			await ((ApiAuthenticationStateProvider)_stateProvider).LoggedOut();
		}

		public async Task<bool> RegisterAsync(string firstName, string lastName, string username, string email, string password)
		{
			var registrationRequest = new RegistrationRequest()
			{
				FirstName = firstName,
				LastName = lastName,
				Email = email,
				UserName = username,
				Password = password
			};
			var response = await _client.RegisterAsync(registrationRequest);
			if (string.IsNullOrEmpty(response.UserId)) return false;

			return true;
		}
	}
}
