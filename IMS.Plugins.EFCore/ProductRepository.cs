using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMSContext _db;

        public ProductRepository(IMSContext iMSContext)
        {
            this._db = iMSContext;
        }

        public async Task AddProductAsync(Product product)
        {
            if (_db.Products.Any(x => x.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase))) return;
             this._db.Add(product);
            await this._db.SaveChangesAsync();   
        }

       
        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
         return await  this._db.Products.Where(x=>(x.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                                                string.IsNullOrWhiteSpace(name)) && x.IsActive).ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            //var prdct = await this._db.Products.FindAsync(id);
            //return prdct;
            return await this._db.Products.
                Include(x=>x.ProductInventories).
                ThenInclude(x=>x.Inventory).
                FirstOrDefaultAsync(x=>x.ProductId == id && x.IsActive);
        }

   
        public async Task UpdateProductAsync(Product product)
        {
            if (_db.Products.Any(x => x.ProductId!= product.ProductId && 
            x.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase))) return;

            var inv = await this._db.Products.FindAsync(product.ProductId);
            if (inv != null)
            {
                inv.Quantity = product.Quantity;
                inv.Price = product.Price;    
                inv.ProductName = product.ProductName;
                inv.ProductInventories = product.ProductInventories;
                await this._db.SaveChangesAsync();
            }
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await this._db.Products.FindAsync(productId);
            if (product != null)
            {
                product.IsActive = false;
                await this._db.SaveChangesAsync();
            }
        }
    }
}