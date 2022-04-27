using Microsoft.AspNetCore.Http;
using Mvc_UI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Mvc_UI.Handler
{
    public class AuthTokenHandler : DelegatingHandler
    {
        public AuthTokenHandler()
        {
        }

        private IHttpContextAccessor _httpContextAccessor;

        public AuthTokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var name = _httpContextAccessor.HttpContext.User.Identity.Name;
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                string _token = _httpContextAccessor.HttpContext.Session.GetString("token");
                if (!String.IsNullOrEmpty(_token))
                {
                    request.Headers.Add("Authorization", $"Bearer {_token}");
                }
            }
            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnAuthorizeException();
            }
            return response;
        }
    }
}
