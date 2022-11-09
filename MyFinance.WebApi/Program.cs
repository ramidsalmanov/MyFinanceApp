using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using MyFinance.Persistence.Extensions;
using MyFinance.WebApi;
using MyFinance.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddAutofacContainer();

var services = builder.Services;

services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson();

services.AddDbContext(builder.Configuration.GetConnectionString("dev"));
services.AddAutoMapper();
services.AddAuthentication(builder.Configuration);

services.AddIdentity(builder.Configuration);
services.AddCors(builder.Configuration);
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();

app.Run();