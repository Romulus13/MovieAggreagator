
using MovieAggreagator.Youtube;
using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Clients;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using DataLayer.Interfaces;
using BusinessLayer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Local");
builder.Services.AddDbContext<MovieAggregatorDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//add services
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<IUnitOfWork, MovieAggregatorDbContext>();
builder.Services.AddSingleton<ISearchFactory>(serviceProvider => new SearchClientFactory(serviceProvider.GetRequiredService<ILogger<SearchClientFactory>>(), serviceProvider.GetRequiredService<IConfiguration>()));
builder.Services.AddTransient<ISearchService, SearchService>();
builder.Services.AddTransient<IImdbDataService, ImdbDataService>();
builder.Services.AddTransient<IYoutubeDataService, YoutubeDataService>();
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
