using UserManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Error Handling (Should be first to catch errors from everything below it)
app.UseMiddleware<ErrorHandlingMiddleware>();

// Swagger (Development only)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authentication (Blocks unauthorized users before logging/processing)
app.UseMiddleware<AuthMiddleware>();

// Logging (Logs the final request/response details)
app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

app.Run();