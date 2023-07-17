using Microsoft.Extensions.Logging;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Providence.Service.Implement
{
    public class ManufacturerCRUD : IServiceCRUD<Manufacturer>
    {
        private readonly DatabaseContext _databaseContext;

        public ManufacturerCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Manufacturer entity)
        {
            try
            {
                _databaseContext.Addresses.Add(entity.Address);
                _databaseContext.SaveChanges();
                entity.AddressId = entity.Address.AddressId;
                _databaseContext.Manufacturers.Add(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var manufacturerEntity = _databaseContext.Manufacturers.FirstOrDefault(m => m.MftId == id);
                if (manufacturerEntity != null)
                {
                    _databaseContext.Manufacturers.Remove(manufacturerEntity);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Manufacturers.Where(m => m.MftId == id).Select(p => new
        {
            mftId = p.MftId,
            mftName = p.MftName,
            address = new
            {
                roadName = p.Address.RoadName,
                ward = new
                {
                    code = p.Address.WardCodeNavigation.Code,
                    name = p.Address.WardCodeNavigation.Name,
                    nameEn = p.Address.WardCodeNavigation.NameEn,
                    fullName = p.Address.WardCodeNavigation.FullName,
                    fullNameEn = p.Address.WardCodeNavigation.FullNameEn,
                    codeName = p.Address.WardCodeNavigation.CodeName,
                    districtCode = p.Address.WardCodeNavigation.DistrictCode,
                    administrativeUnitId = p.Address.WardCodeNavigation.AdministrativeUnitId,
                    //addresses = p.Address.WardCodeNavigation.Addresses,
                    administrativeUnit = p.Address.WardCodeNavigation.AdministrativeUnit,
                },
                district = new
                {
                    code = p.Address.DistrictCodeNavigation.Code,
                    name = p.Address.DistrictCodeNavigation.Name,
                    nameEn = p.Address.DistrictCodeNavigation.NameEn,
                    fullName = p.Address.DistrictCodeNavigation.FullName,
                    fullNameEn = p.Address.DistrictCodeNavigation.FullNameEn,
                    codeName = p.Address.DistrictCodeNavigation.CodeName,
                    provinceCode = p.Address.DistrictCodeNavigation.ProvinceCode,
                    administrativeUnitId = p.Address.DistrictCodeNavigation.AdministrativeUnitId,
                    //addresses = p.Address.DistrictCodeNavigation.Addresses,
                    administrativeUnit = p.Address.DistrictCodeNavigation.AdministrativeUnit,
                },
                province = new
                {
                    code = p.Address.ProvinceCodeNavigation.Code,
                    name = p.Address.ProvinceCodeNavigation.Name,
                    nameEn = p.Address.ProvinceCodeNavigation.NameEn,
                    fullName = p.Address.ProvinceCodeNavigation.FullName,
                    fullNameEn = p.Address.ProvinceCodeNavigation.FullNameEn,
                    codeName = p.Address.ProvinceCodeNavigation.CodeName,
                    administrativeUnitId = p.Address.ProvinceCodeNavigation.AdministrativeUnitId,
                    //addresses = p.Address.ProvinceCodeNavigation.Addresses,
                    administrativeUnit = p.Address.ProvinceCodeNavigation.AdministrativeUnit,
                    administrativeRegionId = p.Address.ProvinceCodeNavigation.AdministrativeRegionId,
                },

            },
            mftDescription = p.MftDescription
        }).FirstOrDefault()!;

        public dynamic Read() => _databaseContext.Manufacturers.Select(p => new
        {
            mftId = p.MftId,
            mftName = p.MftName,
            address = new
            {
                roadName = p.Address.RoadName,
                ward = new
                {
                    code = p.Address.WardCodeNavigation.Code,
                    name = p.Address.WardCodeNavigation.Name,
                    nameEn = p.Address.WardCodeNavigation.NameEn,
                    fullName = p.Address.WardCodeNavigation.FullName,
                    fullNameEn = p.Address.WardCodeNavigation.FullNameEn,
                    codeName = p.Address.WardCodeNavigation.CodeName,
                    districtCode= p.Address.WardCodeNavigation.DistrictCode,
                    administrativeUnitId = p.Address.WardCodeNavigation.AdministrativeUnitId,
                    //addresses = p.Address.WardCodeNavigation.Addresses,
                    administrativeUnit = p.Address.WardCodeNavigation.AdministrativeUnit,
                },
                district = new
                {
                    code = p.Address.DistrictCodeNavigation.Code,
                    name = p.Address.DistrictCodeNavigation.Name,
                    nameEn = p.Address.DistrictCodeNavigation.NameEn,
                    fullName = p.Address.DistrictCodeNavigation.FullName,
                    fullNameEn = p.Address.DistrictCodeNavigation.FullNameEn,
                    codeName = p.Address.DistrictCodeNavigation.CodeName,
                    provinceCode= p.Address.DistrictCodeNavigation.ProvinceCode,
                    administrativeUnitId = p.Address.DistrictCodeNavigation.AdministrativeUnitId,
                    //addresses = p.Address.DistrictCodeNavigation.Addresses,
                    administrativeUnit = p.Address.DistrictCodeNavigation.AdministrativeUnit,
                },
                province = new
                {
                    code = p.Address.ProvinceCodeNavigation.Code,
                    name = p.Address.ProvinceCodeNavigation.Name,
                    nameEn = p.Address.ProvinceCodeNavigation.NameEn,
                    fullName = p.Address.ProvinceCodeNavigation.FullName,
                    fullNameEn = p.Address.ProvinceCodeNavigation.FullNameEn,
                    codeName = p.Address.ProvinceCodeNavigation.CodeName,
                    administrativeUnitId = p.Address.ProvinceCodeNavigation.AdministrativeUnitId,
                    //addresses = p.Address.ProvinceCodeNavigation.Addresses,
                    administrativeUnit = p.Address.ProvinceCodeNavigation.AdministrativeUnit,
                    administrativeRegionId = p.Address.ProvinceCodeNavigation.AdministrativeRegionId,
                },
                
            },
            mftDescription = p.MftDescription
        }).ToList();

        public bool Update(Manufacturer entity)
        {
            try
            {
                _databaseContext.Addresses.Update(entity.Address);
                _databaseContext.SaveChanges();
                entity.AddressId = entity.Address.AddressId;
                _databaseContext.Manufacturers.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
