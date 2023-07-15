using Providence.Models;

namespace Providence.Service.Interface;

public interface IProductService
{
    public dynamic ReadINPQ();

    public dynamic HideProduct(int productId);

    public bool AddProduct(IFormFile[] files,Product product);

}
