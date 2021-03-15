using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tacx.Activities.Api.Adapters;
using Tacx.Activities.Api.Adapters.Cosmos;
using Tacx.Activities.Api.Adapters.Storage;
using Tacx.Activities.Api.Core;
using Tacx.Activities.Api.Core.Adapters;
using Tacx.Activities.Api.Core.Services;
using Tacx.Activities.Api.Core.Services.Activities;

namespace Tacx.Activities.Api
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
            services.AddControllers();
            services.AddOptions<AppConfiguration>()
                .BindConfiguration("");
            services.AddTransient<IActivityRepository, CosmosActivityRepository>();
            services.AddSingleton<CosmosClient>(sp =>
                CosmosClientConnector.CreateClient(
                    sp.GetRequiredService<IOptions<AppConfiguration>>().Value,
                    new DefaultAzureCredential()));
            services.AddTransient<IFileRepository, StorageFileRepository>();
            services.AddSingleton<BlobServiceClient>(sp => BlobStorageConnector.CreateClient(
                sp.GetRequiredService<IOptions<AppConfiguration>>().Value,
                new DefaultAzureCredential()));
            
            ConfigureCoreServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static void ConfigureCoreServices(IServiceCollection services)
        {
            services.AddTransient<IActivityService, ActivityService>();
        }
    }
}
