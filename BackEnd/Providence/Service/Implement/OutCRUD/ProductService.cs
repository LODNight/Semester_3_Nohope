using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Providence.Helper;
using Providence.Models;
using Providence.Service.Interface;
using System.Diagnostics;

namespace Providence.Service.Implement.OutCRUD;

public class ProdupctService : IProductService
{ 
    private readonly DatabaseContext _databaseContext;
    private IConfiguration configuration;
    private IWebHostEnvironment webHostEnvironment;

    public ProdupctService(DatabaseContext databaseContext, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        _databaseContext = databaseContext;
        this.configuration = configuration;
        this.webHostEnvironment = webHostEnvironment;
    }

    public bool AddProduct(IFormFile[] files, Product product)
    {
        try
        {
            _databaseContext.Products.Add(product); 
            _databaseContext.SaveChanges();

            var urls = new List<string>();
            Debug.WriteLine("File Info");
            if (files != null && files.Length > 0)
            {
                foreach (var file in files)
                {
                    // Upload file
                    var fileName = FileHelper.generateFileName(file.FileName);
                    var path = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    urls.Add(configuration["BaseUrl"] + fileName);

                    // ADD Image Product
                    _databaseContext.ProductImages.Add(new ProductImage() {
                        ImageUrl = fileName,
                        ProductId= product.ProductId,
                    } );
                }
            }
            return _databaseContext.SaveChanges() > 0;
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    public dynamic HideProduct(int productId)
    {
        var product = _databaseContext.Products.Find(productId);
        if (product != null)
        {
            product.Hide = !product.Hide;
            return _databaseContext.SaveChanges() > 0;
        }
        else
        {
            return false;
        }
    }

    public dynamic ReadINPQ() => _databaseContext.Products.Select(product => new
    {
        id = product.ProductId,
        name = product.ProductName,
        price = product.Price,
        quantity = product.Quantity,
    });

}
