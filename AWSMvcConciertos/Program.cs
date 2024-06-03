using AWSMvcConciertos.Helpers;
using AWSMvcConciertos.Models;
using AWSMvcConciertos.Services;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

string jsonSecrets =
    await HelperSecretManager.GetSecretsAsync();
KeysModel keys = JsonConvert.DeserializeObject<KeysModel>(jsonSecrets);
builder.Services.AddSingleton<KeysModel>(x => keys);

// Add services to the container.
builder.Services.AddTransient<ServiceApiConciertos>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
