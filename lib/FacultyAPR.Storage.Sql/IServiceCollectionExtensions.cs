using FacultyAPR.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace FacultyAPR.Storage.Sql
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSQLFormStore(this IServiceCollection services)
        { 
            return services
                .AddSingleton<SqlFormStore>()
                .AddSingleton<IFormStructureStore>(sp => sp.GetRequiredService<SqlFormStore>())
                .AddSingleton<IFormContentStore>(sp => sp.GetRequiredService<SqlFormStore>())
                .AddSingleton<IFormReviewerStore, SqlReviewerStore>();
        }

        public static IServiceCollection AddSQLUserStore(this IServiceCollection services)
        { 
            return services.AddSingleton<IUserStore, SqlUserStore>();
        }
    }
}
