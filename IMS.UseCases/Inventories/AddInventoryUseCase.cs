using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases
{
    public class AddInventoryUseCase : IAddInventoryUseCase
    {
        private readonly IInventoryRepository _inventoryRepo;

        public AddInventoryUseCase(IInventoryRepository inventoryRepo)
        {
            this._inventoryRepo = inventoryRepo;
        }
        public async Task ExecuteAsync(Inventory inventory)
        {
            await _inventoryRepo.AddInventoryAsync(inventory);
        }
    }
}
