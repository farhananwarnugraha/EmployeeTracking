using employeetrackingData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace employeetrackingData;

public static class Dempedencies
{
    public static void ConfigurationService(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<EmployeeTrackingContext>(
            option => option.UseSqlServer(configuration.GetConnectionString("EmployeeTrackingConnections"))
        );
    }
}
