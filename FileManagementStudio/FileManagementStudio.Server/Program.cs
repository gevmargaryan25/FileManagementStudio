using FileManagementStudio.DAL.Context;
using FileManagementStudio.DAL.Entities;
using FileManagementStudio.DAL.Repositories.Interfaces;
using FileManagementStudio.Server.Repository;
using FileManagementStudio.Services.Services;
using FileManagementStudio.Services.Services.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("FileManagementStudioDbContextConnection") ?? throw new InvalidOperationException();
builder.Services.AddDbContext<FileManagementStudioDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddAuthentication();


builder.Services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<FileManagementStudioDbContext>();
// Add services to the container.


//Configuration for fluentValidation
builder.Services.AddControllers()
            .AddFluentValidation(v =>
            {
                v.ImplicitlyValidateChildProperties = true;
                v.ImplicitlyValidateRootCollectionElements = true;
                v.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });
//Configuration for automapping
builder.Services.AddAutoMapper(typeof(Program).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IAzureStorage, AzureStorage>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IFileService<FileEntity>, FIleService>();

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


