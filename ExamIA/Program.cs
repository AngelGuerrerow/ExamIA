using ExamIA;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureService(builder.Services);

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();
