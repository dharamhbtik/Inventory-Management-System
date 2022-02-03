using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases
{
    public class ViewInventoryByNameUseCase : IViewInventoriesByNameUseCase
    {
        private readonly IInventoryRepository _inventoryRepository;
        public ViewInventoryByNameUseCase(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }



        public async Task<IEnumerable<Inventory>> ExecuteAsync(string name = "")
        {
            return await _inventoryRepository.GetInventoriesByName(name);
        }
    }
}