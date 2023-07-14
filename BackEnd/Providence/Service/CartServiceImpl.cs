using Providence.Models;
using System.Diagnostics;

namespace Providence.Service;

public class CartServiceImpl : CartService
{
    private DatabaseContext db;
    private IConfiguration configuration;

    public CartServiceImpl(DatabaseContext db, IConfiguration configuration)
    {
        this.db = db;
        this.configuration = configuration;
    }

    public bool Create(Cart cart)
    {
        try
        {
            db.Carts.Add(cart);
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
            db.Carts.Remove(db.Carts.Find(id));
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public bool Edit(Cart cart)
    {
        try
        {
            db.Carts.Update(cart);
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
        return db.Carts.Where(c => c.CartId == id).Select(c => new
        {
            cartId = c.CartId,
            accountId = c.AccountId,
            createAt = c.CreatedAt,
            updateAt = c.UpdatedAt
        }).SingleOrDefault();
    }

    public dynamic findAll()
    {
        return db.Carts.Select(c => new
        {
            cartId = c.CartId,
            accountId = c.AccountId,
            createAt = c.CreatedAt,
            updateAt = c.UpdatedAt
        }).OrderByDescending(c => c.cartId).ToList();
    }
}
