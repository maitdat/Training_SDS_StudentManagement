using Microsoft.EntityFrameworkCore;
using Training_SDS_StudentManagement.Components;
using Training_SDS_StudentManagement.Data;
using Training_SDS_StudentManagement.DesignPattern.Repository;
using Training_SDS_StudentManagement.Service;
using Training_SDS_StudentManagement.Service.ConsoleAppWithDB;
using Training_SDS_StudentManagement.Service.StudentService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
//Service Register
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IConsoleAppWithDBService, ConsoleAppWithDBService>();

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// 1.c Bài tập thực hành quản lý sinh viên(console)
//var consoleApp = new ConsoleService();
//consoleApp.Run();

//3.d Bài tập thực hành áp dụng nguyên lý SOLID, DI, Repository pattern vào chương trình quản lý sinh viên console
using (var scope = app.Services.CreateScope())
{
    var consoleAppWithDB = scope.ServiceProvider.GetRequiredService<IConsoleAppWithDBService>();
    consoleAppWithDB.Run();
}

app.Run();
