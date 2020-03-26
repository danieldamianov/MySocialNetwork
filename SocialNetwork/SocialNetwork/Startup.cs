using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialNetwork.Controllers.Extensions;
using SocialNetwork.Controllers.ImageConvertingFunctionality;
using SocialNetwork.Controllers.TimeSinceCreationFunctionality;
using SocialNetwork.Data;
using SocialNetwork.DatabaseModels;
using SocialNetwork.Services;
using SocialNetwork.Services.CommentsManagement;
using SocialNetwork.Services.FollowingManagement;
using SocialNetwork.Services.LikesManagement;
using SocialNetwork.Services.PostsManagement;
using SocialNetwork.Services.ProfileManagement;

namespace SocialNetwork
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFollowingService, FollowingService>();
            services.AddTransient<IUsersPostsService, UsersPostsService>();
            services.AddTransient<ICommentsFunctionalityService ,CommentsFunctionalityService>();
            services.AddTransient<IProfileManagementService, ProfileManagementService>();
            services.AddTransient<ILikesService, LikesService>();

            services.AddTransient<IControllerAdditionalFunctionality, ControllerAdditionalFunctionality>();

            services.AddSingleton<ImageConverter>();
            services.AddSingleton<TimeConvertingService>();

            services.AddDbContext<SocialNetworkDbContext>(options =>
            options.UseSqlServer(
                    this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<SocialNetworkUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<SocialNetworkDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            // services.ConfigureApplicationCookie(options => options.LoginPath = "/");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
