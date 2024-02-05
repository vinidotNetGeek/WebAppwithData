using Microsoft.FeatureManagement;
using WebAppwithData.Services;

var builder = WebApplication.CreateBuilder(args);
var appConfigConnString = "Endpoint=https://azureappconfig1291.azconfig.io;Id=iqsP;Secret=fFV9H2EsSdPUNXX9GnTJME8jWxBmhzaFaJh/APxgkj0=";
builder.Host.ConfigureAppConfiguration(builder =>
{
    builder.AddAzureAppConfiguration( options =>
    {
        options.Connect(appConfigConnString).UseFeatureFlags();
    });
});

builder.Services.AddRazorPages();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddFeatureManagement();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
