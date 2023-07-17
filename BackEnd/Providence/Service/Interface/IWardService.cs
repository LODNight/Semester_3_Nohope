using Providence.Models;

namespace Providence.Service.Interface;

public interface IWardService
{
    public dynamic FindWardByDistrict(string code);

}
