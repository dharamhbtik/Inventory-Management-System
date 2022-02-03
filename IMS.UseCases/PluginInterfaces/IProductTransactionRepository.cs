using IMS.CoreBusiness;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IProductTransactionRepository
    {
        Task ProduceAsync(string prodcutionNumber,Product product, int quantity,double price, string doneBy);
        Task SellProductAsync(string salesOrderNumber, Product product, int quanitty, double price, string doneBy);
        Task<IEnumerable<ProductTransaction>> GetProductTransactionsAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? productTransactionType);
    }
}
