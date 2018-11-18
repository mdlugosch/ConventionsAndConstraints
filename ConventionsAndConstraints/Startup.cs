using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConventionsAndConstraints.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ConventionsAndConstraints
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UserAgentComparer>();
            services.AddMvc().AddMvcOptions(options =>
            {
                //options.Conventions.Add(new ActionNamePrefixAttribute("Do"));
                //options.Conventions.Add(new AdditionalActionsAttribute());
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
