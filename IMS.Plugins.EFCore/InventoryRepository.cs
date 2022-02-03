using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IMSContext iMSContext;

        public InventoryRepository(IMSContext iMSContext)
        {
            this.iMSContext = iMSContext;
        }

        public async Task AddInventoryAsync(Inventory inventory)
        {
            if (iMSContext.Inventories.Any(x => x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase))) return;
             this.iMSContext.Add(inventory);
            await this.iMSContext.SaveChangesAsync();   
        }

        public async Task<IEnumerable<Inventory>> GetInventoriesByName(string name)
        {
         return await  this.iMSContext.Inventories.Where(x=>x.InventoryName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                                                string.IsNullOrWhiteSpace(name)).ToListAsync();
        }

        public async Task<Inventory> GetInventoryById(int id)
        {
            var inv = await this.iMSContext.Inventories.FindAsync(id);
            return inv;
        }

        public async Task UpdateInventory(Inventory inventory)
        {
            if (iMSContext.Inventories.Any(x => x.InventoryId!= inventory.InventoryId && x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase))) return;

            var inv = this.iMSContext.Inventories.Find(inventory.InventoryId);
            if (inv != null)
            {
                inv.Quantity = inventory.Quantity;
                inv.Price = inventory.Price;    
                inv.InventoryName = inventory.InventoryName;
                await this.iMSContext.SaveChangesAsync();
            }
        }
    }
}