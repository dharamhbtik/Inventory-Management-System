using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore
{
    public class InventoryTransactionRepository : IInventoryTRansactionRepository
    {
        private readonly IMSContext _db;

        public InventoryTransactionRepository(IMSContext iMSContext)
        {
            this._db = iMSContext;
        }

        public async Task<IEnumerable<InventoryTransaction>> GetInventoryTransactionsAsync(
            string inventoryName, 
            DateTime? dateFrom, 
            DateTime? dateTo, 
            InventoryTransactionType? transactionType)
        {
            var query = from it in _db.InventoryTransactions
                        join inv in _db.Inventories on it.InventoryId equals inv.InventoryId
                        where (string.IsNullOrWhiteSpace(inventoryName) || inv.InventoryName.Contains(inventoryName, StringComparison.OrdinalIgnoreCase)) &&
                        (!dateFrom.HasValue || it.TransactionDate >= dateFrom) &&
                        (!dateTo.HasValue || it.TransactionDate <= dateTo) &&
                        (!transactionType.HasValue || it.ActivityType == transactionType)
                        select it;
            return await query.ToListAsync();
        }
        public async Task PurchaseAsync(string poNumber, Inventory inventory, int quantity, double price,string doneBy)
        {
            this._db.InventoryTransactions.Add(new InventoryTransaction
            {
                PONumber = poNumber,
                InventoryId = inventory.InventoryId,
                QuanittyBefore = inventory.Quantity,
                ActivityType    = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = inventory.Quantity+quantity,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy,
                UnitPrice = price
            });
            await this._db.SaveChangesAsync();
        }
    }
}