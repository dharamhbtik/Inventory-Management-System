using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases
{
    public class EditInventoryUseCase : IEditInventoryUseCase
    {
        private readonly IInventoryRepository _repository;

        public EditInventoryUseCase(IInventoryRepository repository)
        {
            this._repository = repository;
        }
        public async Task ExecuteAsync(Inventory inventory)
        {
            await this._repository.UpdateInventory(inventory);
        }
    }
}
