using DbConnection;
using DbConnection.Models;
using Helpers;

namespace Services
{
    public class CatalogService : ICatalogService
    {
        public static DataContext _dataContext;
        private readonly ICacheService _cacheService;

        public CatalogService(ICacheService cacheService, DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Products GetProduct(Guid id)
        {
            try
            {
                Products product = new Products();
                var productList = _cacheService.Get<List<Products>>("ProductList");
                if (productList != null)
                {
                    product = productList.First(x => x.ProductId == id);
                }
                else
                {
                    product = _dataContext.Products.First(x => x.ProductStatus == ProductStatuses.Active && x.ProductId == id);
                }
                return product;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Products> GetProducts()
        {
            try
            {
                var productList = _cacheService.Get<List<Products>>("ProductList");
                if (productList == null)
                {
                    productList = _dataContext.Products.Where(x => x.ProductStatus == ProductStatuses.Active).ToList();

                    _cacheService.Set("ProductList", productList, TimeSpan.FromMinutes(30));
                }

                return productList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool AddProduct(Products product)
        {
            try
            {
                _dataContext.Products.Add(product);

                _cacheService.Remove("ProductList");

                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
    }

    public interface ICatalogService
    {
        public List<Products> GetProducts();
        public Products GetProduct(Guid id);
        public bool AddProduct(Products product);

    }
}