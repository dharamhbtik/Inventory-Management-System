using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class SellProductUseCase : ISellProductUseCase
    {
        private readonly IProductTransactionRepository productTransactionRepository;
        private readonly IProductRepository productRepository;

        public SellProductUseCase(
            IProductTransactionRepository productTransactionRepository,
            IProductRepository productRepository)
        {
            this.productTransactionRepository = productTransactionRepository;
            this.productRepository = productRepository;
        }

        public async Task ExecuteAsync(string salesOrderNumber, Product product, int quanitty, string doneBy)
        {
            await this.productTransactionRepository.SellProductAsync(salesOrderNumber, product, quanitty, product.Price, doneBy);
            product.Quantity -= quanitty;
            await this.productRepository.UpdateProductAsync(product);
        }
    }
}
