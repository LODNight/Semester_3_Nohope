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

    public dynamic FindDistrictByProvince(string code)
    {
        return _databaseContext.Districts.Where(p => p.ProvinceCode == code).Select(p => new
        {
            code = p.Code,
            name = p.Name,
            nameEn = p.NameEn,
            fullName = p.FullName,
            fullNameEn = p.FullNameEn,
            codeName = p.CodeName,
            provinceCode = p.ProvinceCode,
            administrativeUnitId = p.AdministrativeUnitId,
        }).ToList();
    }
}
