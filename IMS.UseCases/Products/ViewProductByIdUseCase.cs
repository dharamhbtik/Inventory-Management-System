using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases
{
    public class ViewProductByIdUseCase : IViewProductByIdUseCase
    {
        private readonly IProductRepository _productRepository;
        public ViewProductByIdUseCase(IProductRepository inventoryRepository)
        {
            _productRepository = inventoryRepository;
        }
        public async Task<Product> ExecuteAsync(int id)
        {
            return await _productRepository.GetProductById(id);
        }
    }
}