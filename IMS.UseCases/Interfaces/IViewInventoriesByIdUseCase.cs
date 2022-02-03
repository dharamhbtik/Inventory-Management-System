using IMS.CoreBusiness;

namespace IMS.UseCases
{
    public interface IViewInventoriesByIdUseCase
    {
        Task<Inventory> ExecuteAsync(int id);
    }
}