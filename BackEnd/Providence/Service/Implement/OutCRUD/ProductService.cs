using Providence.Models;
using Providence.Service.Interface;

namespace Providence.Service.Implement.OutCRUD;

public class ProductService : IProductService
{ 
    private readonly DatabaseContext _databaseContext;

    public ProductService(DatabaseContext databaseContext)
    {
            _databaseContext = databaseContext;
    }

    public dynamic ReadINPQ() => _databaseContext.Products.Select(product => new
    {
        id = product.ProductId,
        name = product.ProductName,
        price = product.Price,
        quantity = product.Quantity,
    });

}
