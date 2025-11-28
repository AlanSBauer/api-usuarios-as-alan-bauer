using System.Reflection.Metadata;
using System.Runtime.Intrinsics.Arm;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=app.db"));

var app = builder.Build();

app.UseHttpsRedirection();


app.Run();