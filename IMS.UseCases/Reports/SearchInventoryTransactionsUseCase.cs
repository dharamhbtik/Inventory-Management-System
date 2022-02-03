using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.Reports
{
    public class SearchInventoryTransactionsUseCase : ISearchInventoryTransactionsUseCase
    {
        private readonly IInventoryTRansactionRepository inventoryTRansactionRepository;

        public SearchInventoryTransactionsUseCase(IInventoryTRansactionRepository inventoryTRansactionRepository)
        {
            this.inventoryTRansactionRepository = inventoryTRansactionRepository;
        }
        public async Task<IEnumerable<InventoryTransaction>> ExecuteAsync(
            string inventoryName,
            DateTime? dateFrom,
            DateTime? dateTo,
            InventoryTransactionType? transactionType)
        {
            return await this.inventoryTRansactionRepository.GetInventoryTransactionsAsync(
                inventoryName,
                dateFrom,
                dateTo,
                transactionType);
        }
    }
}
