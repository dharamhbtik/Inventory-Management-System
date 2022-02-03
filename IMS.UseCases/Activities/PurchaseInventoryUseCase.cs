using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases
{
    public class PurchaseInventoryUseCase : IPurchaseInventoryUseCase
    {
        private readonly IInventoryTRansactionRepository _purchaseRepo;
        private readonly IInventoryRepository inventoryRepository;

        public PurchaseInventoryUseCase(IInventoryTRansactionRepository purchaseRepo,
            IInventoryRepository inventoryRepository)
        {
            _purchaseRepo = purchaseRepo;
            this.inventoryRepository = inventoryRepository;
        }



        public async Task ExecuteAsync(string poNumber, Inventory inventory, int quantity, string doneBy)
        {
            await this._purchaseRepo.PurchaseAsync(poNumber, inventory, quantity, inventory.Price, doneBy);
            inventory.Quantity+=quantity;
            await inventoryRepository.UpdateInventory(inventory);
        }
    }
}