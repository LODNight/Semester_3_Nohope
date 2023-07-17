using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Providence.Helper;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Diagnostics;

namespace Providence.Service.Implement.OutCRUD;

public class WardService : IWardService
{
    private readonly DatabaseContext _databaseContext;
    private readonly IConfiguration configuration;
    private IWebHostEnvironment webHostEnvironment;

    public WardService(DatabaseContext databaseContext, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        _databaseContext = databaseContext;
        this.configuration = configuration;
        this.webHostEnvironment = webHostEnvironment;
    }

    public dynamic FindWardByDistrict(string code)
    {
        return _databaseContext.Wards.Where(p => p.DistrictCode == code).Select(p => new
        {
            code = p.Code,
            name = p.Name,
            nameEn = p.NameEn,
            fullName = p.FullName,
            fullNameEn = p.FullNameEn,
            codeName =p.CodeName,
            districtCode = p.DistrictCode,
            administrativeUnitId = p.AdministrativeUnitId,
        }).ToList();
    }
}
