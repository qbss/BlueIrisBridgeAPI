using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueIrisBridge.Infrastructure;
using BlueIrisBridge.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using Microsoft.Extensions.Options;

namespace BlueIrisBridge
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
      services.AddOptions();
      services.Configure<AppSettings>(Configuration);
      services.AddLogging(logging =>
      {
        logging.AddEventLog();
      });
      services.AddSingleton<IApiKeyAuthorization, ApiKeyAuthorization>();

      services.AddScoped<IBlueIrisService, BlueIrisService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }
      

      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}
