using FileManagementStudio.DAL.Context;
using FileManagementStudio.DAL.Entities;
using FileManagementStudio.DAL.Repositories;
using FileManagementStudio.DAL.Repositories.Interfaces;
using FileManagementStudio.Server.Repository;
using FileManagementStudio.Services.Services;
using FileManagementStudio.Services.Services.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Claims;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("FileManagementStudioDbContextConnection") ?? throw new InvalidOperationException();
builder.Services.AddDbContext<FileManagementStudioDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<FileManagementStudioDbContext>();

builder.Services.AddControllers()
            .AddFluentValidation(v =>
            {
                v.ImplicitlyValidateChildProperties = true;
                v.ImplicitlyValidateRootCollectionElements = true;
                v.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });



// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

//Configuration for fluentValidation
//Configuration for automapping
builder.Services.AddAutoMapper(typeof(Program).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IAzureStorage, AzureStorage>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IFileService<FileEntity>, FIleService>();
builder.Services.AddScoped<IFileEntityRepository, FileEntityRepository>();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
