using Microsoft.Extensions.Logging;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Providence.Service.Implement
{
    public class PaymentMethodCRUD : IServiceCRUD<PaymentMethod>
    {
        private readonly DatabaseContext _databaseContext;

        public PaymentMethodCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(PaymentMethod entity)
        {
            try
            {
                _databaseContext.PaymentMethods.Add(entity);
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
                var paymentMethodEntity = _databaseContext.PaymentMethods.FirstOrDefault(pm => pm.PaymentId == id);
                if (paymentMethodEntity != null)
                {
                    _databaseContext.PaymentMethods.Remove(paymentMethodEntity);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.PaymentMethods.Where(pm => pm.PaymentId == id).Select(p => new
        {
            paymentId = p.PaymentId,
            paymentName = p.PaymentName,
            createdAt = p.CreatedAt,
            updatedAt = p.UpdatedAt,
        }).FirstOrDefault()!;

        public dynamic Read() => _databaseContext.PaymentMethods.Select(p => new
        {
            paymentId = p.PaymentId,
            paymentName = p.PaymentName,
            createdAt = p.CreatedAt,
            updatedAt = p.UpdatedAt,
        }).ToList();

        public bool Update(PaymentMethod entity)
        {
            try
            {
                _databaseContext.PaymentMethods.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
