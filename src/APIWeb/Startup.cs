using AutoMapper;
using Core;
using Core.Interfaces.Services;
using EmailService;
using Infraestructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using Services;

namespace APIWeb
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
            // requires using Microsoft.Extensions.Options
            services.Configure<WebsiteDatabaseSettings>(
                Configuration.GetSection(nameof(WebsiteDatabaseSettings)));

            services.AddSingleton<IWebsiteDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<WebsiteDatabaseSettings>>().Value);

            var conventionPack = new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                //new IgnoreExtraElementsConvention(true)
            };
            ConventionRegistry.Register("CustomConventions", conventionPack, _ => true);

            #region Database Repositories

            services.AddSingleton<AboutMeRepository>();
            services.AddSingleton<WorkRepository>();
            services.AddSingleton<ProjectRepository>();
            services.AddSingleton<ContactRepository>();
            services.AddSingleton<BlogRepository>();

            #endregion

            #region Database Services

            services.AddScoped<IWorkService, WorkService>();
            services.AddScoped<IProfessionalDataService, ProfessionalDataService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IBlogService, BlogService>();

            services.AddScoped<IEmailSender, EmailSender>();

            #endregion

            services.AddControllers()
                .AddJsonOptions(options => 
                {
                    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                });

            services.AddCors(o => o.AddPolicy("NoCors", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("NoCors");
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
