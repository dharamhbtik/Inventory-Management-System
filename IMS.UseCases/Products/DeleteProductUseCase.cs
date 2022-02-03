using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases
{
    public class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly IProductRepository productRepository;

        public DeleteProductUseCase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task ExecuteAsync(int productId)
        {
            if (productId<=0) return;
            await this.productRepository.DeleteProductAsync(productId);
        }
    }
}
