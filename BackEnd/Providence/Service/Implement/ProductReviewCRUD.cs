using Microsoft.Extensions.Logging;
using Providence.Models;
using Providence.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Providence.Service.Implement
{
    public class ProductReviewCRUD : IServiceCRUD<ProductReview>
    {
        private readonly DatabaseContext _databaseContext;

        public ProductReviewCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(ProductReview entity)
        {
            try
            {
                _databaseContext.ProductReviews.Add(entity);
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
                var productReviewEntity = _databaseContext.ProductReviews.FirstOrDefault(pr => pr.ProductReviewId == id);
                if (productReviewEntity != null)
                {
                    _databaseContext.ProductReviews.Remove(productReviewEntity);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.ProductReviews.FirstOrDefault(pr => pr.ProductReviewId == id);

        public dynamic Read() => _databaseContext.ProductReviews.ToList();

        public bool Update(ProductReview entity)
        {
            try
            {
                _databaseContext.ProductReviews.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
