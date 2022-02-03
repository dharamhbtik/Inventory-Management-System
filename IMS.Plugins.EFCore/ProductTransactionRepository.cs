using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore
{
    public class ProductTransactionRepository : IProductTransactionRepository
    {
        private readonly IMSContext _db;
        private readonly IProductRepository productRepository;

        public ProductTransactionRepository(IMSContext iMSContext,IProductRepository productRepository)
        {
            this._db = iMSContext;
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductTransaction>> GetProductTransactionsAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? productTransactionType)
        {
            var query = from it in _db.ProductTransactions
                        join inv in _db.Products on it.ProductId equals inv.ProductId
                        where (string.IsNullOrWhiteSpace(productName) || inv.ProductName.Contains(productName, StringComparison.OrdinalIgnoreCase)) &&
                        (!dateFrom.HasValue || it.TransactionDate >= dateFrom) &&
                        (!dateTo.HasValue || it.TransactionDate <= dateTo) &&
                        (!productTransactionType.HasValue || it.ActivityType == productTransactionType)
                        select it;
            return await query.ToListAsync();
        }

        public async Task ProduceAsync(string productionNUmber, Product product,int quantity,double price,string doneBy)
        {
            var prod = await this.productRepository.GetProductById(product.ProductId);
            if (prod != null)
            {
                foreach (var pi in prod.ProductInventories)
                {
                    pi.Inventory.Quantity -= quantity * pi.InventoryQuantity;
                    this._db.InventoryTransactions.Add(new InventoryTransaction
                    {
                        ProductionNumber = productionNUmber,
                        InventoryId = pi.Inventory.InventoryId,
                        QuanittyBefore = pi.Inventory.Quantity,
                        ActivityType = InventoryTransactionType.ProduceProduct,
                        QuantityAfter = pi.Inventory.Quantity,
                        TransactionDate = DateTime.Now,
                        DoneBy = doneBy,
                        UnitPrice = price
                    });
                }
            }

           

            this._db.ProductTransactions.Add(new ProductTransaction
            {
                ProductionNumber = productionNUmber,
                ProductId = product.ProductId,
                QuanittyBefore= product.Quantity,
                ActivityType =  ProductTransactionType.ProduceProduct,
                QuantityAfter=product.Quantity+ quantity,
                DoneBy=doneBy ,
                TransactionDate = DateTime.Now,
                UnitPrice = price 
            });
            await this._db.SaveChangesAsync();
        }

        public async Task SellProductAsync(string salesOrderNumber, Product product, int quanitty, double price, string doneBy)
        {
            this._db.ProductTransactions.Add(new ProductTransaction
            {
                SalesOrderNumber = salesOrderNumber,
                ProductId = product.ProductId,
                QuanittyBefore = product.Quantity,
                QuantityAfter = product.Quantity-quanitty,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy,
                UnitPrice= price
            });
            await this._db.SaveChangesAsync();
        }
    }
}