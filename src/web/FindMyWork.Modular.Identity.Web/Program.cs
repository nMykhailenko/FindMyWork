using FindMyWork.Modules.Identity.Core.Application.Common.Contracts.Extensions;
using FindMyWork.Shared.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSharedInfrastructure();
builder.Services.AddCore();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSharedInfrastructure();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseOpenIdDict();

app.MapControllers();
app.MapRazorPages();
app.MapDefaultControllerRoute();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();