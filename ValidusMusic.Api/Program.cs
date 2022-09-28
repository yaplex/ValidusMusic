using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ValidusMusic.Api.Controllers;
using ValidusMusic.Core.Domain;
using ValidusMusic.Core.Domain.Repository;
using ValidusMusic.Core.ExternalContracts.DataTransfer.Artist;
using ValidusMusic.DataProvider;
using ValidusMusic.DataProvider.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ValidusMusicDbContext>(options =>
{
    // if (builder.Environment.IsDevelopment())
    // {
    //     options.UseInMemoryDatabase("ValidusMusic");
    //
    //     using (var ctx = new ValidusMusicDbContext(options.Options))
    //     {
    //         ctx.Database.EnsureCreatedAsync();
    //     }
    //     using (var ctx = new ValidusMusicDbContext(options.Options))
    //     {
    //         ctx.Database.EnsureCreatedAsync();
    //     }
    // }
    // else
    // {
    // }

    options.UseSqlServer(builder.Configuration.GetConnectionString("ValidusMusicConnection"),
        b => b.MigrationsAssembly("ValidusMusic.DataProvider"));



});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assembliesToScan = new List<Assembly>
{
    typeof(ArtistController).Assembly,
    typeof(ArtistRepository).Assembly,
    typeof(ArtistDto).Assembly,
    typeof(Artist).Assembly,
}.ToArray();


builder.Services.AddMediatR(assembliesToScan);
builder.Services.AddAutoMapper(assembliesToScan);

builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<ISongRepository, SongRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
