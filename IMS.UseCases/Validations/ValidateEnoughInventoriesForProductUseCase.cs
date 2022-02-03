using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class ValidateEnoughInventoriesForProductUseCase : IValidateEnoughInventoriesForProductUseCase
    {
        private readonly IProductRepository productRepository;

        public ValidateEnoughInventoriesForProductUseCase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<bool> ExecuteAsync(Product product, int quantity)
        {
            var prod = await productRepository.GetProductById(product.ProductId);
            foreach (var item in prod.ProductInventories)
            {
                if (item.InventoryQuantity * quantity > item.Inventory.Quantity)
                    return false;
            }
            return true;
        }
    }
}
