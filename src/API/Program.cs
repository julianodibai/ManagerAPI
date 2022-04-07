using System.Text;
using API.Token;
using API.Token.Interfaces;
using API.ViewModels;
using AutoMapper;
using Domain.Entities;
using Infra.Context;
using Infra.Repositories;
using Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.DTO;
using Services.Services;
using Services.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region DI

builder.Services.AddSingleton(d => builder.Configuration);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();

#endregion

#region AutoMapper

var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>().ReverseMap();
                cfg.CreateMap<CreateUserViewModel, UserDTO>().ReverseMap();
                cfg.CreateMap<UpdateUserViewModel, UserDTO>().ReverseMap();
            });

builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

#endregion


#region Database

var connectionString = builder.Configuration.GetConnectionString("UserManager");

builder.Services.AddDbContext<ManagerContext>(options => options
        .UseSqlServer(connectionString),
        ServiceLifetime.Transient
        );

#endregion

#region Jwt

var secretKey = builder.Configuration["Jwt:Key"];  

builder.Services.AddAuthentication(x =>
{
     x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})   
    .AddJwtBearer(x =>
    {				 
        x.RequireHttpsMetadata = false; 
        x.SaveToken = true;				 
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
								
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

#endregion

#region Swagger

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Manager API",
        Version = "v1",
        Description = "API construída para fins de treino.",

        Contact = new OpenApiContact
        {
            Name = "Juliano Dibai",
            Email = "dibaijuliano@gmail.com",
            Url = new Uri("https://github.com/julianodibai")
        },

    });
			
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor utilize Bearer <TOKEN>",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement 
    {
        {
            new OpenApiSecurityScheme
	        {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
           
			new string[] { }

        }
    });

});

#endregion


var app = builder.Build();

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
