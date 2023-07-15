﻿using Microsoft.Extensions.Logging;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Providence.Service.Implement
{
    public class WishlistCRUD : IServiceCRUD<Wishlist>
    {
        private readonly DatabaseContext _databaseContext;

        public WishlistCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Wishlist entity)
        {
            try
            {
                _databaseContext.Wishlists.Add(entity);
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
                var wishlistEntity = _databaseContext.Wishlists.FirstOrDefault(w => w.WishlistId == id);
                if (wishlistEntity != null)
                {
                    _databaseContext.Wishlists.Remove(wishlistEntity);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Wishlists.FirstOrDefault(w => w.WishlistId == id);

        public dynamic Read() => _databaseContext.Wishlists.Select(p => new
        {
            wishlistId = p.WishlistId,
            accountId = p.AccountId,
            accountName = p.Account.Firstname + ' ' + p.Account.Lastname,
            productId = p.ProductId,
            productName = p.Product.ProductName,
            createdAt = p.CreatedAt,
            updatedAt = p.UpdatedAt,
        }).ToList();

        public bool Update(Wishlist entity)
        {
            try
            {
                _databaseContext.Wishlists.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
