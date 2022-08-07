using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Primitives;

namespace WebsocketTest.Factories
{
    /// <summary>
    /// It contains logic for using the token as a URI parameter.
    /// Mostly debugger class for adding breakpoints and checking out errors which are not visible on the UI.
    /// </summary>
    public static class JwtBearerEventFactory
    {
        public static JwtBearerEvents Get() => new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                return Task.CompletedTask;
            },
            OnForbidden = context =>
            {
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                var failure = context.AuthenticateFailure;
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                return Task.CompletedTask;
            },
            OnMessageReceived = context =>
            {
                if (context.Request.Query.TryGetValue("access_token", out StringValues values))
                {
                    //IMPOTANT: The below line blows up the whole Token Validation part if you keep Bearer in it. You will get "No SecurityTokenValidator available for token."
                    context.Token = values.Single().Replace("Bearer","").Trim(); //clean up token.
                }

                return Task.CompletedTask;
            }
        };
    }
}
