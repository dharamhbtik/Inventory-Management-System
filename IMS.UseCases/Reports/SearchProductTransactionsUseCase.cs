using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Reports
{
    public class SearchProductTransactionsUseCase : ISearchProductTransactionsUseCase
    {
        private readonly IProductTransactionRepository productTransactionRepository;

        public SearchProductTransactionsUseCase(IProductTransactionRepository productTransactionRepository)
        {
            this.productTransactionRepository = productTransactionRepository;
        }
        public async Task<IEnumerable<ProductTransaction>> ExecuteAsync(
            string productName,
            DateTime? dateFrom,
            DateTime? dateTo,
            ProductTransactionType? productTransactionType)
        {
            return await productTransactionRepository.GetProductTransactionsAsync(productName,
                dateFrom,
                dateTo,
                productTransactionType);
        }
    }
}
