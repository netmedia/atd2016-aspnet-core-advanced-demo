using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATD2016.Services
{
    public interface IBasicAuthenticationParserService
    {
        BasicAuthenticationParserResult Parse(HttpContext context);
    }
}
