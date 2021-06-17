using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using PTI.Microservices.Library.Configuration;
using PTI.Microservices.Library.Interceptors;
using PTI.Microservices.Library.Services;
using System.Linq;
using System.Threading.Tasks;

namespace FairPlayDating.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAdB2C"));
            services.Configure<MicrosoftIdentityOptions>(JwtBearerDefaults.AuthenticationScheme, options => 
            {
                options.Scope.Add("user_photos");
            });
            services.Configure<JwtBearerOptions>(
                JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters.NameClaimType = "name";
                    options.Events.OnTokenValidated = (context) =>
                    {
                        return Task.CompletedTask;
                    };
                });

            GlobalPackageConfiguration.RapidApiKey = Configuration["RapidApiKey"];
            GlobalPackageConfiguration.EnableHttpRequestInformationLog = false;
            services.AddTransient<CustomHttpClientHandler>();
            services.AddTransient<CustomHttpClient>();
            services.AddTransient<FacebookGraphService>( sp=> 
            {
                var httpContextAccesor = sp.GetRequiredService<IHttpContextAccessor>();
                var claims = httpContextAccesor.HttpContext.User.Claims;
                var idpAccessToken = claims.Single(p => p.Type == "idp_access_token").Value;
                FacebookGraphConfiguration facebookGraphConfiguration =
                new FacebookGraphConfiguration()
                {
                    AccessToken = idpAccessToken
                };
                FacebookGraphService facebookGraphService = new FacebookGraphService(null,
                    facebookGraphConfiguration, sp.GetRequiredService<CustomHttpClient>());
                return facebookGraphService;
            });
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseExceptionHandler(cfg =>
            {
                cfg.Run(async context =>
                {
                    var exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();
                    var error = exceptionHandlerPathFeature.Error;
                    if (error != null)
                    {
                        //try
                        //{
                        //    FairplaytubeDatabaseContext fairplaytubeDatabaseContext =
                        //    this.CreateFairPlayTubeDbContext(context.RequestServices);
                        //    await fairplaytubeDatabaseContext.ErrorLog.AddAsync(new ErrorLog()
                        //    {
                        //        FullException = error.ToString(),
                        //        StackTrace = error.StackTrace,
                        //        Message = error.Message
                        //    });
                        //    await fairplaytubeDatabaseContext.SaveChangesAsync();
                        //}
                        //catch (Exception)
                        //{
                        //    throw;
                        //}
                        //ProblemHttpResponse problemHttpResponse = new()
                        //{
                        //    Detail = error.Message,
                        //};
                        //await context.Response.WriteAsJsonAsync<ProblemHttpResponse>(problemHttpResponse);
                    }
                });
            });

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
