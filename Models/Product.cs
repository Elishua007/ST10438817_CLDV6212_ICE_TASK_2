using Azure;
using Azure.Data.Tables;

namespace ICE_TASK_2.Models
{
    public class Product : ITableEntity
    {
        public string PartitionKey { get; set; } = "Products";
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Clearance { get; set; }
        public ETag ETag { get; set; } = ETag.All;
        public DateTimeOffset? Timestamp { get; set; }
    }
}
