using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Providence.Models;
using Providence.Service.Interface;

namespace Providence.Service.Implement.OutCRUD;

public class AccountService : IAccountService
{
    private readonly DatabaseContext _databaseContext;
    private IConfiguration configuration;
    private IWebHostEnvironment webHostEnvironment;

    public AccountService(DatabaseContext databaseContext, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        _databaseContext = databaseContext;
        this.configuration = configuration;
        this.webHostEnvironment = webHostEnvironment;
    }

    

    public bool ChangePass([FromBody] ChangePass changePass)
    { 

        var account = _databaseContext.Accounts.FirstOrDefault(p => p.Email == changePass.Email);
        if (account != null)
        {

            if (!BCrypt.Net.BCrypt.Verify(changePass.CurrentPass, account.Password))
            {
                return false;

            }
            var hashedNewPass = BCrypt.Net.BCrypt.HashPassword(changePass.NewPass);
            account.Password = hashedNewPass;
            return _databaseContext.SaveChanges() > 0;

        }
        return false;
    }

    // Show Account
    public dynamic ShowAccountActive() => _databaseContext.Accounts.Where(p => p.Status == true).Select(acc => new
    {
        accountId = acc.AccountId,
        firstName = acc.Firstname,
        lastName = acc.Lastname,
        email = acc.Email,
        password = acc.Password,
        avatar = configuration["BaseUrl"] + "/images/" + acc.Avatar,
        roleId = acc.RoleId,
        roleName = acc.Role.RoleName,
        status = acc.Status,
        securityCode = acc.SecurityCode,
        createdAt = acc.CreatedAt,
        updatedAt = acc.UpdatedAt,
    }).ToList();

    public bool CheckMail(string email)
    {
        return _databaseContext.Accounts.Count(p => p.Email == email) > 0;
    }

    public dynamic VerifyCode(string account)
    {
        try
        {
            var accountEntity = _databaseContext.Accounts.FirstOrDefault(a => a.Email == account);
            if (accountEntity != null)
            {
                accountEntity.Status = true;
                _databaseContext.SaveChanges();
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public bool Active(Verify verify)
    {
        try
        {
            var account = _databaseContext.Accounts.FirstOrDefault(a => a.Email == verify.Email && a.SecurityCode == verify.SecurityCode);
            if (account == null)
            {
                return false;
            }
            account.Status = true;
            _databaseContext.Accounts.Update(account);
            return _databaseContext.SaveChanges() > 0;
        }
        catch
        {
            return false;

        }
    }
}
