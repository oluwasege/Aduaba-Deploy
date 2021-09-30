using Aduaba.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Services.Interfaces
{
    public interface IMailServices
    {
        Task SendEmailAsync(string email, string subject, string content);
        Task<AuthenticationResponse> ConfirmEmailAsync(string userId, string token);
    }
}
