using ClientManagementAPI.Data;
using ClientManagementAPI.Hubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews(); 
builder.Services.AddSignalR();
builder.Services.AddRazorPages(); 

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<NotificationHub>("/notificationHub");
    endpoints.MapRazorPages();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"); 
});

using (var scope = app.Services.CreateScope()) // тестовое заполнение базы
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Clients.Any(c => c.Email == "semrnoff@ml.ru"))
    {
        context.Clients.Add(new Client
        {
            Name = "Семенов Петр Петрович",
            Email = "semrnoff@ml.ru",
            Message = "Нужна консультация!",
            AddedAt = DateTime.Now
        });
        context.SaveChanges();
    }
}


app.Run();
