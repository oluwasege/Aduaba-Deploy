using Aduaba.DTO;
using Aduaba.DTO.Account;
using Aduaba.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Services
{
    public interface IUserServices
    {
        Task<AuthenticationResponse> RegisterUserAsync(RegisterRequest model);
        Task<AuthenticationResponse> LoginAsync(LoginRequest model);
        Task<AuthenticationResponse> UpdateAsync(UpdateRequest model);
        Task<string> DeleteUserAsync();
       // Task<AuthenticationResponse> ForgotPasswordAsync(string email);
     Task<AuthenticationResponse> SendPasswordResetTokenSmsAsync(string email);
        Task<AuthenticationResponse> SendEmailResetTokenEmailAsync(string email);
       Task<AuthenticationResponse> ResetPasswordAsync(ResetPassword model);
        
        Task<AuthenticationResponse> RefreshTokenAsync(string token);
        public bool RevokeRefreshToken(string token);
        //Task<string> LogoutAsync();
    }
}
