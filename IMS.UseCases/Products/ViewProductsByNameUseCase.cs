using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases
{
    public class ViewProductsByNameUseCase : IViewProductsByNameUseCase
    {
        private readonly IProductRepository _productRepository;
        public ViewProductsByNameUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }



        public async Task<IEnumerable<Product>> ExecuteAsync(string name = "")
        {
            return await _productRepository.GetProductsByName(name);
        }
    }
}