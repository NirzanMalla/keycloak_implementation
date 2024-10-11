using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<MongoDatabaseSettings>(builder.Configuration.GetSection("MongoDatabaseSettings"));

builder.Services.AddSingleton<IMongoClient, MongoClient>(s =>
{
    var mongoDbSettings = s.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value;
    return new MongoClient(mongoDbSettings.ConnectionString);
});

builder.Services.AddScoped(s =>
{
    var client = s.GetRequiredService<IMongoClient>();
    var mongoDbSettings = s.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value;
    return client.GetDatabase(mongoDbSettings.DatabaseName);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();



app.Run();
