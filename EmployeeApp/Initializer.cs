namespace EmployeeApp
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class Initializer
    {
        public static IServiceCollection AddSyneData(this IServiceCollection services, IConfiguration config)
        {
            var conn = config.GetConnectionString("DefaultData");
            if (string.IsNullOrEmpty(conn))
                services.AddDbContext<EmployeeDbContext>(options => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Employee;Integrated Security=True;MultipleActiveResultSets=True"));
            else
                services.AddDbContext<EmployeeDbContext>(options => options.UseSqlServer(conn));
            return services;
        }
    }
}