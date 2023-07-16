using Providence.Models;

namespace Providence.Service.Interface;

public interface IDistrictService
{
    public dynamic FindWardByDistrict(int idWard);
}
