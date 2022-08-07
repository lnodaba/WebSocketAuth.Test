using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using WebsocketTest.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

//JwtSecurityTokenHandler

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.Authority = "https://localhost:44341/";
    options.RequireHttpsMetadata = false;
    options.IncludeErrorDetails = true;
    //IMPOTANT: IS4 doesn't properly generate AUD claim we disable the validation on it
    options.TokenValidationParameters.ValidateAudience = false; 
    options.Events = JwtBearerEventFactory.Get();
});


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseWebSockets();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
