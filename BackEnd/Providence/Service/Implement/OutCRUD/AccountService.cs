using Providence.Models;
using Providence.Service.Interface;

namespace Providence.Service.Implement.OutCRUD;

public class AccountService : IAccountService
{
    private readonly DatabaseContext _databaseContext;
    private IConfiguration configuration;
    private IWebHostEnvironment webHostEnvironment;

    public Service(DatabaseContext databaseContext, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        _databaseContext = databaseContext;
        this.configuration = configuration;
        this.webHostEnvironment = webHostEnvironment;
    }
    public dynamic ShowAccount() => 
}
