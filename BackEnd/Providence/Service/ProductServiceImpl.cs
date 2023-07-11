using Providence.Models;
using System.Diagnostics;

namespace Providence.Service;

public class ProductServiceImpl : ProductService
{
    private DatabaseContext db;
    private IConfiguration configuration;

    public ProductServiceImpl(DatabaseContext db, IConfiguration configuration)
    {
        this.db = db;
        this.configuration = configuration;
    }

    public bool Create(Product product)
    {
        try
        {
            db.Products.Add(product);
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public bool Delete(int id)
    {
        try
        {
            db.Products.Remove(db.Products.Find(id));
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public bool Edit(Product product)
    {
        try
        {
            db.Products.Update(product);
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public dynamic find(int id)
    {
        return db.Products.Where(p => p.ProductId == id).Select(p => new
        {
            product_id = p.ProductId,
            product_name = p.ProductName,
            price = p.Price,
            categoryId = p.CategoryId,
            description = p.Description,
            quantity = p.Quantity,
            detail = p.Detail,
            exprireDate = p.ExpireDate,
            manufacturerId = p.ManufacturerId,
            hide = p.Hide,
            createdAt = p.CreatedAt,
            updateddAt = p.UpdatedAt
        }).SingleOrDefault();
    }

    public dynamic findAll()
    {
        return db.Products.Select(p => new
        {
            product_id = p.ProductId,
            product_name = p.ProductName,
            price = p.Price,
            categoryId = p.CategoryId,
            categoryName = p.Category.CategoryName,
            description = p.Description,
            quantity = p.Quantity,
            detail = p.Detail,
            exprireDate = p.ExpireDate,
            manufacturerId = p.ManufacturerId,
            hide = p.Hide,
            createdAt = p.CreatedAt,
            updatedAt = p.UpdatedAt
        }).OrderByDescending(p => p.product_id).ToList();
    }
}
