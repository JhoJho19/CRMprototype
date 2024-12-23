var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient("ClientManagementAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7282/api/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});


builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

app.Run();
