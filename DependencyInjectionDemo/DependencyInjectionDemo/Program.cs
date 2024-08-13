using DependencyInjectionDemo.Models;
using DependencyInjectionDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ITransientService, TransientService>();
builder.Services.AddScoped<IScopedService, ScopedService>();
builder.Services.AddSingleton<ISingletonService, SingletonService>();

builder.Services.AddTransient<ServiceA>();
builder.Services.AddTransient<ServiceB>();

builder.Services.AddTransient<Func<string, IService>>(serviceProvider => key =>
{
    return key switch
    {
        Constants.ServiceA => serviceProvider.GetRequiredService<ServiceA>(),
        Constants.ServiceB => serviceProvider.GetRequiredService<ServiceB>(),
        _ => throw new KeyNotFoundException(),
    };
});

builder.Services.AddKeyedSingleton<IKeyedMessageService, KeyedServiceA>(Constants.KeyedServiceA);
builder.Services.AddKeyedSingleton<IKeyedMessageService, KeyedServiceB>(Constants.KeyedServiceB);

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
