using Microsoft.EntityFrameworkCore;
using NHibernate;
using ProtoBuf.Grpc.Server;
using SeverGrpc_NHibernate.Components;
using SeverGrpc_NHibernate.Data;
using SeverGrpc_NHibernate.NHibernateHelper;
using SeverGrpc_NHibernate.RepositoryNHibernate;
using SeverGrpc_NHibernate.Service;
using ISession = NHibernate.ISession;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

//EF core
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

//NHibernate
builder.Services.AddSingleton<ISessionFactory>(SessionFactoryBuilder.CreateSessionFactory());
builder.Services.AddScoped<ISession>(sp => sp.GetRequiredService<ISessionFactory>().OpenSession());

builder.Services.AddTransient(typeof(INHibernateRepository<>), typeof(NHibernateRepository<>));

builder.Services.AddCodeFirstGrpc(config => { config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal; });


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

app.UseCors("AllowAll");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

app.UseGrpcWeb(new GrpcWebOptions() { DefaultEnabled = true });


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<MyService>();
    endpoints.MapGrpcService<StudentService>();
    endpoints.MapGrpcService<ClassService>();
    // ...
});

app.Run();
