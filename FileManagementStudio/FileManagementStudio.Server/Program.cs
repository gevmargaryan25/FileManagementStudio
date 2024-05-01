using FileManagementStudio.DAL.Context;
using FileManagementStudio.DAL.Entities;
using FileManagementStudio.Services.Services;
using FileManagementStudio.Services.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("FileManagementStudioDbContextConnection") ?? throw new InvalidOperationException();
builder.Services.AddDbContext<FileManagementStudioDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddAuthentication();

builder.Services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<FileManagementStudioDbContext>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
//app.MapIdentityApi<User>();

//app.MapPost("/logout", async (SignInManager<User> signInManager) =>
//{
//    await signInManager.SignOutAsync();
//    return Results.Ok;
//}).RequireAuthorization();

app.MapPost("/logout", async (SignInManager<User> signInManager) => 
{ 
    await signInManager.SignOutAsync(); 
    return Results.Ok(); 
});

app.MapGet("/pingauth", (ClaimsPrincipal user) =>
{
    var email = user.FindFirstValue(ClaimValueTypes.Email);
    return Results.Json(new { Email = email });
}).RequireAuthorization();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
