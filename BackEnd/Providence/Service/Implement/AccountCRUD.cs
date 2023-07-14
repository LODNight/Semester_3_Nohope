using Microsoft.Extensions.Logging;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Providence.Service.Implement
{
    public class AccountCRUD : IServiceCRUD<Account>
    {
        private readonly DatabaseContext _databaseContext;

        public AccountCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Account entity)
        {
            try
            {
                _databaseContext.Accounts.Add(entity);
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
                var accountEntity = _databaseContext.Accounts.FirstOrDefault(a => a.AccountId == id);
                if (accountEntity != null)
                {
                    _databaseContext.Accounts.Remove(accountEntity);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

       public dynamic Get(int id) => _databaseContext.Accounts.Where(acc => acc.AccountId == id).Select(acc => new
        {
            accountId = acc.AccountId,
            firstname = acc.Firstname,
            lastname = acc.Lastname,
            email = acc.Email,
            password = acc.Password,
            phoneNumber = acc.Phone,
            gender = acc.Gender,
            address = acc.Address.RoadName,
            avatar = acc.Avatar,
            roleid = acc.RoleId,
            rolename = acc.Role.RoleName,
            status = acc.Status,
            securitycode = acc.SecurityCode,
            createdAt = acc.CreatedAt,
            updatedAt = acc.UpdatedAt,
        }).FirstOrDefault()!;

        public dynamic Read() => _databaseContext.Accounts.Select(acc => new
        {
            accountId = acc.AccountId,
            firstname = acc.Firstname,
            lastname = acc.Lastname,
            email = acc.Email,
            password = acc.Password,
            phoneNumber = acc.Phone,
            gender = acc.Gender,
            address = acc.Address.RoadName,
            avatar = acc.Avatar,
            roleid = acc.RoleId,
            rolename = acc.Role.RoleName,
            status = acc.Status,
            securitycode = acc.SecurityCode,
            createdAt = acc.CreatedAt,
            updatedAt = acc.UpdatedAt,
        }).ToList();

        public bool Update(Account entity)
        {
            try
            {
                _databaseContext.Accounts.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
