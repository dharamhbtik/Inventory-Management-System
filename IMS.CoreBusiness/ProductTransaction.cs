using IMS.CoreBusiness.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CoreBusiness
{
    public class ProductTransaction
    {
        public int ProductTransactionId { get; set; }
        [Required]
        public ProductTransactionType ActivityType { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int QuanittyBefore { get; set; }
        [Required]
        public int QuantityAfter { get; set; }

        public double? UnitPrice { get; set; }
        public string? SalesOrderNumber { get; set; }
        public string? ProductionNumber { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public string DoneBy { get; set; }

        public Product Product { get; set; }
    }
}
