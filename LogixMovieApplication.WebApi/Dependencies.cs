using LogixMovieApplication.Context.Services;
using LogixMovieApplication.WebApi.Data;
using LogixMovieApplication.WebApi.Entities;
using LogixMovieApplication.WebApi.Interfaces;
using LogixMovieApplication.WebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace LogixMovieApplication.WebApi
{
    public static class Dependencies
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(c =>
             c.UseSqlite(configuration["ConnectionStrings:ApplicationConnection"]));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IMovieReactionService, MovieReactionService>();
            services.AddScoped<DbInitializer>();
        }
    }
}
