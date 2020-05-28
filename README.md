# planAndTest

## Hangfire tips
1. use asp.net core web project
2. project file
```
<ItemGroup>
  <PackageReference Include="Microsoft.AspNetCore.App" />
  <PackageReference Include="Hangfire.Core" Version="1.7.*" />
  <PackageReference Include="Hangfire.SqlServer" Version="1.7.*" />
  <PackageReference Include="Hangfire.AspNetCore" Version="1.7.*" />
</ItemGroup>
```
3. create a sql server database for hangfire
4. change appsettings.json 
```
{
  "ConnectionStrings": {
    "HangfireConnection": "Server=.;Database=zHangfire;User Id=sa;Password=sa"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Hangfire": "Information"
    }
  }
}
```
5. Startup.cs
```
public void ConfigureServices(IServiceCollection services)
{
    // Add Hangfire services.
    services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            UsePageLocksOnDequeue = true,
            DisableGlobalLocks = true
        }));

    // Add the processing server as IHostedService
    services.AddHangfireServer();

    .....
}
public void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs, IHostingEnvironment env)
{
    // ...
    app.UseStaticFiles();

    app.UseHangfireDashboard();
    backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));

    .....
}
```
6. run web and Hangfire virtual directory should be the dashboard

## change log
### 2020/5/29
1. call to console done
2. call in progress and call done, and delete
3. 
### 2020/5/24
1. plan changed, use Hangfire, to solve web running console application issues
2. update menu items
