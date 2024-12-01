using MongoDB.Driver;
using StudentMongo.Data;
using StudentMongo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Configure<StudentDatabaseSettings>(
    builder.Configuration.GetSection("ConnectionStrings"));


builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
    return new MongoClient(connectionString);
});



builder.Services.AddSingleton<StudentService>();
builder.Services.AddSingleton<StudentDashboardService>();
builder.Services.AddSingleton<MarkService>();




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
