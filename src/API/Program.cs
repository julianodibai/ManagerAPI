using API.ViewModels;
using AutoMapper;
using Domain.Entities;
using Infra.Context;
using Infra.Repositories;
using Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.DTO;
using Services.Services;
using Services.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region AutoMapper

var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>().ReverseMap();
                cfg.CreateMap<CreateUserViewModel, UserDTO>().ReverseMap();
                //cfg.CreateMap<UpdateUserViewModel, UserDTO>().ReverseMap();
            });

builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

#endregion

#region DI

builder.Services.AddSingleton(d => builder.Configuration);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

#endregion

#region Database

var connectionString = builder.Configuration.GetConnectionString("UserManager");

builder.Services.AddDbContext<ManagerContext>(options => options
        .UseSqlServer(connectionString),
        ServiceLifetime.Transient
        );

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
