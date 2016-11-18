using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace ATD2016.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task AuthenticateAsync(string username, string password)
        {
            if (username == "admin" && password == "123")
                return Task.FromResult(0);

            throw new InvalidCredentialException();
        }
    }
}
