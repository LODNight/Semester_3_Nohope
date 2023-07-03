
using Providence.Models;

namespace Providence.Service;


public interface AccountService
{
    public bool login(string email, string password);

    public bool register(Account account);
    public bool updateInformation(Account account);
    public bool changePass(Account account);

    // ====== Find / Search / Filter
    // Find
    public dynamic findAll();
    public Account findByIdNoTracking(int id);
    public dynamic findbyEmail(string email);
    public dynamic findbyName(string keyword);
    public dynamic findbyStatus(bool status);
}
