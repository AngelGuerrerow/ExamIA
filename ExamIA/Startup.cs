using ExamIA.BL.Services.Service;
using ExamIA.BL.Models.Entities;
using ExamIA.BL.Models.Dtos;
using ExamIA.BL.Contexts;
using ExamIA.BL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ExamIA
{
    public class Startup
    {
        public Startup( IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureService(IServiceCollection services)
        {
            // Add services to the container.

            //services.AddControllers();

            services.AddTransient<SolicitudService>();
            services.AddTransient<MagoService>();
            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<Solicitud, SolicitudDto>();
                configuration.CreateMap<SolicitudDto, Solicitud>();
                configuration.CreateMap<Solicitud, NuevaSolicitudDto>();
                configuration.CreateMap<NuevaSolicitudDto, Solicitud>();
                configuration.CreateMap<Mago, MagoDto>();
                configuration.CreateMap<MagoDto, Mago>();
            }, typeof(Startup));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "ExamIA Apis");
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
