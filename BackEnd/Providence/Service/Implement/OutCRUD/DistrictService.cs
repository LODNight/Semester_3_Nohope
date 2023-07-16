using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Providence.Helper;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Diagnostics;

namespace Providence.Service.Implement.OutCRUD;

public class DistrictService : IDistrictService
{
    private readonly DatabaseContext _databaseContext;
    private readonly IConfiguration configuration;
    private IWebHostEnvironment webHostEnvironment;

    public DistrictService(DatabaseContext databaseContext, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        _databaseContext = databaseContext;
        this.configuration = configuration;
        this.webHostEnvironment = webHostEnvironment;
    }

    public dynamic FindWardByDistrict(int idWard)
    {
        throw new NotImplementedException();
    }
}
