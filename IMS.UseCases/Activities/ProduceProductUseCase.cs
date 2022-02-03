using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class ProduceProductUseCase : IProduceProductUseCase
    {
        private readonly IInventoryRepository inventoryRepository;
        private readonly IProductRepository productRepository;
        private readonly IInventoryTRansactionRepository inventoryTRansactionRepository;
        private readonly IProductTransactionRepository productTransactionRepository;

        public ProduceProductUseCase(
            IInventoryRepository inventoryRepository,
            IProductRepository productRepository,
            IInventoryTRansactionRepository inventoryTRansactionRepository,
            IProductTransactionRepository productTransactionRepository)
        {
            this.inventoryRepository = inventoryRepository;
            this.productRepository = productRepository;
            this.inventoryTRansactionRepository = inventoryTRansactionRepository;
            this.productTransactionRepository = productTransactionRepository;
        }
        public async Task ExecuteAsync(string productionNUmber, Product product, int quantity,string doneBy)
        {
            //await this.inventoryRepository.
            await this.productTransactionRepository.ProduceAsync(productionNUmber, product, quantity,product.Price, doneBy);
            product.Quantity+=quantity;
            await this.productRepository.UpdateProductAsync(product);

        }
    }
}
