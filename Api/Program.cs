using API.ProgramSetup;
using API.ProgramSetup.ipNXSalesPortalApis.AppStartUpConfig;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.ConfigureLogging();

// Add services to the container.
builder.Services.SetupDependencyInjection(builder.Configuration);
builder.Services.ConfigureJwtAuthentication(builder.Configuration);
builder.Services.FileUploadSetUpServices();
builder.Services.ConfigureSwagger();
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 30 * 1024 * 1024; // 30 MB
});
//builder.Services.AddHangfire(configuration =>
//    configuration.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("SalesPortalDbConnection")));
var app = builder.Build();
////Configure Hangfire
//app.UseHangfireDashboard(); // Enable Hangfire dashboard
//app.ConfigureHangfire(builder.Configuration.GetConnectionString("SalesPortalDbConnection"))


// Apply Database Migrations
app.ApplyDatabaseMigrations();

//Configuring the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseGlobalExceptionHandler();
app.MapControllers();

app.Run();

 