

using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Providence.Models;
using System.Diagnostics;
using System.Globalization;

namespace Providence.Service;

public class AccountServiceImpl : AccountService
{
    private DatabaseContext db;
    private IConfiguration configuration;
    private AccountService accountService;

    public AccountServiceImpl(DatabaseContext _db, IConfiguration configuration)
    {
        db = _db;
        this.configuration = configuration;
    }

    // Create 
    public bool register(Account account)
    {
        try
        {
            db.Accounts.Add(account);
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;   
        }
    }

    // Login
    public bool login(string email, string password)
    {
        try
        {
            var account = db.Accounts.SingleOrDefault(a => a.Email == email);
            if (account != null)
            {
                return BCrypt.Net.BCrypt.Verify(password, account.Password);
            }
            return false;
        }
        catch
        {
            return false;
        }
    }


    // Update Information
    public bool updateInformation(Account account)
    {
        try
        {
            db.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    // Change Password
    public bool changePass(Account account)
    {
        try
        {
            db.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    // For Update If dont change password
    public Account findByIdNoTracking(int id)
    {
        return db.Accounts.AsNoTracking().SingleOrDefault(a => a.AccountId == id);
    }

    // Delete 
    public bool Delete(int id)
    {
        try
        {
            db.Accounts.Remove(db.Accounts.Find(id));
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    // ============================== 
    // FInd
    // ============================== 

    // Find All
    public dynamic findAll()
    {
        return db.Accounts.Select(acc => new
        {
            accountId = acc.AccountId,
            firstname = acc.Firstname,
            lastname = acc.Lastname,
            email = acc.Email,
            phoneNumber = acc.Phone,
            gender = acc.Gender,
            address = acc.Address,
            avatar = acc.Avatar,
            roleid = acc.RoleId,
            rolename = acc.Role.RoleName,
            status = acc.Status,
            createdAt = acc.CreatedAt,
            updatedAt = acc.UpdatedAt,
        }).ToList();
    }

    // Find Email
    public dynamic findbyEmail(string email)
    {
        return db.Accounts.Where(p => p.Email == email).Select(acc => new
        {
            id = acc.AccountId,
            firstname = acc.Firstname,
            lastname = acc.Lastname,
            email = acc.Email,
            phone = acc.Phone,
            gender = acc.Gender,
            address = acc.Address,
            avatar = acc.Avatar,
            roleid = acc.RoleId,
            rolename = acc.Role.RoleName,
            status = acc.Status,
            createdAt = acc.CreatedAt,
            updatedAt = acc.UpdatedAt,
        }).ToList();
    }

    public dynamic findbyName(string keyword)
    {
        return db.Accounts.Where(
            p => p.Firstname == keyword || 
            p.Lastname == keyword || 
            (p.Firstname + ' ' +  p.Lastname) == keyword)
            .Select(acc => new
        {
            id = acc.AccountId,
            firstname = acc.Firstname,
            lastname = acc.Lastname,
            email = acc.Email,
            phone = acc.Phone,
            gender = acc.Gender,
            address = acc.Address,
            avatar = acc.Avatar,
            roleid = acc.RoleId,
            rolename = acc.Role.RoleName,
            status = acc.Status,
            createdAt = acc.CreatedAt,
            updatedAt = acc.UpdatedAt,
        }).ToList();
    }

    public dynamic findbyStatus(bool status)
    {
        return db.Accounts.Where(p => p.Status == status).Select(acc => new
        {
            id = acc.AccountId,
            firstname = acc.Firstname,
            lastname = acc.Lastname,
            email = acc.Email,
            phone = acc.Phone,
            gender = acc.Gender,
            address = acc.Address,
            avatar = acc.Avatar,
            roleid = acc.RoleId,
            rolename = acc.Role.RoleName,
            status = acc.Status,
            createdAt = acc.CreatedAt,
            updatedAt = acc.UpdatedAt,
        }).ToList();
    }

}
