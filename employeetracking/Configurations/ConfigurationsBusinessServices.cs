using employeetracking.Service;
using employeetrackingBusiness.Interfaces;
using employeetrackingBusiness.Repositories;
using System;

namespace employeetracking.Configurations;

public static class ConfigurationsBusinessServices
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection service)
    {
        service.AddScoped<IJabatan, JabatanRpository>();
        service.AddScoped<JabatanService>();
        service.AddScoped<JabtanInportService>();
        service.AddScoped<ICabang, CabangRepository>();
        service.AddScoped<CabangService>();
        service.AddScoped<CabangImportService>();
        service.AddScoped<IPegawai, PegawaiRepository>();
        service.AddScoped<PegawaiService>();
        service.AddScoped<PegawaiImportService>();
        return service;
    }
}
