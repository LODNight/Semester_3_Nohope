namespace Providence.Service;

public interface AddressService
{
    public dynamic findWardsByDistrict(string district);
    public dynamic findDistrictByProvince(string province);
    public dynamic findAllProvince();
}
