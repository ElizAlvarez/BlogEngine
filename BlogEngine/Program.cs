using System.Text;
using BlogEngine.Authorization;
using BlogEngine.Core.Interfaces;
using BlogEngine.Data.Context;
using BlogEngine.Data.Repositories;
using BlogEngine.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    var config = builder.Configuration;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization(opt => {
    opt.AddPolicy(AuthorizationData.RolePublicPolicy, p =>
    p.RequireClaim(AuthorizationData.RoleClaim, "1"));

    opt.AddPolicy(AuthorizationData.RoleWriterPolicy, p =>
    p.RequireClaim(AuthorizationData.RoleClaim, "2"));

    opt.AddPolicy(AuthorizationData.RoleEditorPolicy, p =>
    p.RequireClaim(AuthorizationData.RoleClaim, "3"));

    opt.AddPolicy(AuthorizationData.AllRoles, p =>
        p.RequireClaim(AuthorizationData.RoleClaim, new[] { "1", "2", "3" }));
});

// Add services to the container.
builder.Services.AddDbContext<BlogEngineDbContext>();
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

app.Run();

