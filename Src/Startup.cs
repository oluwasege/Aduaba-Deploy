using Aduaba.Data;
using Aduaba.Models;
using Aduaba.Services;
using Aduaba.Services.CardService;
using Aduaba.Services.Interfaces;

using Aduaba.Setings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aduaba
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
            services.AddRazorPages();
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );


            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //add Swagger UI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aduaba", Version = "v1" });
            });

            //Configuration from AppSettings
            services.Configure<JWT>(Configuration.GetSection("JWT"));
            //User Manager Service
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddScoped<IOrderSimi, OrderSimiServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddTransient<IMailServices, MailServices>();
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<ISearchServices, SearchServices>();
                
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IVendorServices, VendorServices>();
            services.AddScoped<ICardControl, CardControl>();
            services.AddScoped<IHttpCurrentUser, HttpCurrentUser>();
            services.AddScoped<ICartServicesSeg, CartServicesSeg>();
           services.AddScoped<IWishListServiceSeg, WishListServiceSeg>();
           // services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IShippingAddressService, ShippingAddressService>();
            services.AddScoped<IOrderServices, OrderServices>();
            // services.AddScoped(sp => ShoppingCartActions.GetCart(sp));
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
          //services.AddScoped<IProductServices, ProductServices>();
            //Adding DB Context with MSSQL
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("Aduaba"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredLength = 4;
                option.Password.RequireLowercase = false;
                option.SignIn.RequireConfirmedEmail = false;
                option.SignIn.RequireConfirmedPhoneNumber = false;
                

            });

            //Adding Athentication - JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = true;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,

                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
                    };
                });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aduaba v1"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            var scope = app.ApplicationServices.CreateScope();
             var context = scope.ServiceProvider.GetService<ApplicationDbContext>();


            app.UseHttpsRedirection();
            app.UseStaticFiles();
           
            // app.UseSession();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
               
            });
        }
    }
}
