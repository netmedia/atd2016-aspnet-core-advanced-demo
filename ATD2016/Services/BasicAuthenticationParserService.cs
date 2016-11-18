using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATD2016.Services
{
    public class BasicAuthenticationParserService : IBasicAuthenticationParserService
    {
        public BasicAuthenticationParserResult Parse(HttpContext context)
        {
            var credentials = _GetCredentials(context);

            return new BasicAuthenticationParserResult { Username = _GetValue(credentials, 0), Password = _GetValue(credentials, 1) };
        }

        private static string _GetValue(string credentials, int index)
        {
            if (string.IsNullOrWhiteSpace(credentials))
                return null;

            var parts = credentials.Split(':');

            return parts.Length == 2 ? parts[index] : null;
        }

        private string _GetCredentials(HttpContext context)
        {
            try
            {
                StringValues authHeader;
                if (context.Request.Headers.TryGetValue("Authorization", out authHeader) &&
                    authHeader.Any() &&
                    authHeader[0].StartsWith("Basic "))
                {
                    var value = Convert.FromBase64String(authHeader[0].Split(' ')[1]);
                    return Encoding.UTF8.GetString(value);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
