// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ebrain.admin.bc;
using ebrain.admin.bc.Models;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Newtonsoft.Json;
using ebrain.admin.bc.Core;
using ebrain.admin.bc.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Ebrain.ViewModels;
using Ebrain.Helpers;
using Ebrain.Policies;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OAuth.Validation;
using Microsoft.AspNetCore.Identity;
using Swashbuckle.AspNetCore.Swagger;
using AppPermissions = ebrain.admin.bc.Core.ApplicationPermissions;
using DinkToPdf.Contracts;
using DinkToPdf;

namespace Ebrain
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add a new middleware validating access tokens issued by the server.
            //services.AddAuthentication()
            //    .AddOAuthValidation()
            //    // Add a new middleware issuing tokens.
            //    .AddOpenIdConnectServer(options =>
            //    {
            //        options.AllowInsecureHttp = false;
            //    });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"], b => b.MigrationsAssembly("ebrain.admin.web"));
                options.UseOpenIddict();
            });

            // add identity
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configure Identity options and password complexity here
            services.Configure<IdentityOptions>(options =>
            {
                // User settings
                options.User.RequireUniqueEmail = true;

                //    //// Password settings
                //    //options.Password.RequireDigit = true;
                //    //options.Password.RequiredLength = 8;
                //    //options.Password.RequireNonAlphanumeric = false;
                //    //options.Password.RequireUppercase = true;
                //    //options.Password.RequireLowercase = false;

                //    //// Lockout settings
                //    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                //    //options.Lockout.MaxFailedAccessAttempts = 10;

                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            // Register the OpenIddict services.
            services.AddOpenIddict(options =>
            {
                options.AddEntityFrameworkCoreStores<ApplicationDbContext>();
                options.AddMvcBinders();
                options.EnableTokenEndpoint("/connect/token");
                options.AllowPasswordFlow();
                options.AllowRefreshTokenFlow();
                options.DisableHttpsRequirement();
                //options.AddSigningKey(new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(Configuration["STSKey"])));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = OAuthValidationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OAuthValidationDefaults.AuthenticationScheme;
            }).AddOAuthValidation();//.AddOpenIdConnectServer(options =>
            //{
            //    options.UseSlidingExpiration = true;
            //    options.AllowInsecureHttp = false;
            //    options.ApplicationCanDisplayErrors = true;
            //    //options.SigningCredentials.AddKey(new RsaSecurityKey(rsa));
            //});

            // Add cors
            services.AddCors();

            // Add framework services.
            services.AddMvc();

            //Todo: ***Using DataAnnotations for validation until Swashbuckle supports FluentValidation***
            //services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            //.AddJsonOptions(opts =>
            //{
            //    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //});

            EmailSender.Configuration = new SmtpConfig
            {
                Host = Configuration["SmtpConfig:Host"],
                Port = int.Parse(Configuration["SmtpConfig:Port"]),
                UseSSL = bool.Parse(Configuration["SmtpConfig:UseSSL"]),
                Name = Configuration["SmtpConfig:Name"],
                Username = Configuration["SmtpConfig:Username"],
                EmailAddress = Configuration["SmtpConfig:EmailAddress"],
                Password = Configuration["SmtpConfig:Password"]
            };

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("BearerAuth", new ApiKeyScheme
                {
                    Name = "Authorization",
                    Description = "Login with your bearer authentication token. e.g. Bearer <auth-token>",
                    In = "header",
                    Type = "apiKey"
                });

                c.SwaggerDoc("v1", new Info { Title = "Ebrain API", Version = "v1" });
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthPolicies.ViewUserByUserIdPolicy, policy => policy.Requirements.Add(new ViewUserByIdRequirement()));

                options.AddPolicy(AuthPolicies.ViewUsersPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ViewUsers));

                options.AddPolicy(AuthPolicies.ManageUserByUserIdPolicy, policy => policy.Requirements.Add(new ManageUserByIdRequirement()));

                options.AddPolicy(AuthPolicies.ManageUsersPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageUsers));

                options.AddPolicy(AuthPolicies.ViewRoleByRoleNamePolicy, policy => policy.Requirements.Add(new ViewRoleByNameRequirement()));

                options.AddPolicy(AuthPolicies.ViewRolesPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ViewRoles));

                options.AddPolicy(AuthPolicies.AssignRolesPolicy, policy => policy.Requirements.Add(new AssignRolesRequirement()));

                options.AddPolicy(AuthPolicies.ManageRolesPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageRoles));
            });

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            // Repositories
            services.AddScoped<IUnitOfWork, HttpUnitOfWork>();
            services.AddScoped<IAccountManager, AccountManager>();

            // Auth Policies
            services.AddSingleton<IAuthorizationHandler, ViewUserByIdHandler>();
            services.AddSingleton<IAuthorizationHandler, ManageUserByIdHandler>();
            services.AddSingleton<IAuthorizationHandler, ViewRoleByNameHandler>();
            services.AddSingleton<IAuthorizationHandler, AssignRolesHandler>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            // DB Creation and Seeding
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug(LogLevel.Warning);
            loggerFactory.AddFile(Configuration.GetSection("Logging"));

            Utilities.ConfigureLogger(loggerFactory);
            EmailTemplates.Initialize(env);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //Configure Cors
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.ApplicationJson;

                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        string errorMsg = JsonConvert.SerializeObject(new { error = error.Error.Message });
                        await context.Response.WriteAsync(errorMsg).ConfigureAwait(false);
                    }
                });
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ebrain API V1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
