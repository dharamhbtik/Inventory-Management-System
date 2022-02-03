using IMS.CoreBusiness;

namespace IMS.UseCases
{
    public interface IProduceProductUseCase
    {
        Task ExecuteAsync(string productionNUmber, Product product, int quantity, string doneBy);
    }
}