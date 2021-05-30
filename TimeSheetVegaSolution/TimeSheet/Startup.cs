using BusinessLogic.Implementation;
using DataAccess.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Contracts.Interface.Repository;
using Contracts.Interface.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Model.Email;
using Microsoft.AspNetCore.Http.Features;

namespace TimeSheet
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
            var emailConfig = Configuration
            .GetSection("EmailConfiguration")
            .Get<EmailConfiguration>();
                services.AddSingleton(emailConfig);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = Configuration["Jwt:Issuer"],
                   ValidAudience = Configuration["Jwt:Issuer"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
               };
           });

            services.AddControllers();

            services.AddAuthorization();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TimeSheetProject", Version = "v1" });
            });
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IDailyTimeSheetService, DailyTimeSheetService>();
            services.AddScoped<IDailyTimeSheetRepository, DailyTimeSheetRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IEmailSender, EmailSender>();

            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:3000");
                builder.AllowAnyHeader();
                builder.WithExposedHeaders("Number-Of-Pages", "Content-Disposition");
                builder.AllowAnyMethod();
                builder.AllowCredentials();
                builder.Build();

            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("MyPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TimeSheetProject v1"));
            }

            app.UseExceptionHandler("/error");
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
