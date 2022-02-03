using IMS.CoreBusiness;

namespace IMS.UseCases
{
    public interface IValidateEnoughInventoriesForProductUseCase
    {
        Task<bool> ExecuteAsync(Product product, int quantity);
    }
}