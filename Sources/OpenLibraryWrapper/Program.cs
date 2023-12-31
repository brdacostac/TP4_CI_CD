﻿using System.Reflection;
using DtoAbstractLayer;
using LibraryDTO;
using Microsoft.OpenApi.Models;
using MyLibraryManager;
using OpenLibraryClient;
using StubbedDTO;

var builder = WebApplication.CreateBuilder(args);


var envar = Environment.GetEnvironmentVariable("DTO_MANAGER");

if (envar == null || envar == "stub")
{
    builder.Services.AddSingleton<IDtoManager, Stub>();
}
else if (envar == "api")
{
    builder.Services.AddSingleton<IDtoManager, OpenLibClientAPI>();
}
else if (envar == "mariadb")
{
    builder.Services.AddSingleton<IDtoManager, MyLibraryMgr>();
}

// Add services to the container.

builder.Services.AddSingleton<IDtoManager, Stub>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();