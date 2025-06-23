using BudgetAppProject.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

DIExtension.AddUsecases(builder.Services);
DIExtension.AddDataAccess(builder.Services);
DIExtension.AddPublishers(builder.Services);
DIExtension.AddSubscribers(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();