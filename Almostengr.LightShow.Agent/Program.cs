using Almostengr.LightShow.Agent.Services;
using Almostengr.FalconPiPlayer.ApiClient.Common.Shared;
using Almostengr.LightShow.Agent.Services.AppSettingsManager;
using Almostengr.LightShow.Agent.Services.CurrentStatusManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration
    .AddJsonFile("appsettings.json", false, true);

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();
builder.Services.AddAppSettingsServices();
builder.Services.AddCurrentStatusServices();
builder.Services.AddFalconPiPlayerApiClientServices(builder.Configuration);

builder.Services.AddHostedService<AgentWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();