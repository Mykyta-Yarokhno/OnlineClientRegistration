using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineClientRegistration.DataModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddRazorPages();
var app = builder.Build();

//var context = new DbContextOptionsBuilder<ApplicationDbContext>();

//using (var db = new ApplicationDbContext(context.UseSqlite("Data Source=onlineClientRegistration.db").EnableSensitiveDataLogging().Options))
//{

//    var ci = db.Clients.Where(client => client.Name == "WOMEN").FirstOrDefault();
//    db.Records.Add(new Record
//    {
//        DateAndTime = DateTime.Now,
//        ClientInfo = ci,
//        ServicesRequested = new(){ db.ServicesTypes.FirstOrDefault() },
//    });

//    db.SaveChanges();
//}

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
