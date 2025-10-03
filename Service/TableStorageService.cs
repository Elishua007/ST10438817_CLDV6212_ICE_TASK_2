using Azure.Data.Tables;
using ICE_TASK_2.Models;

namespace ICE_TASK_2.Service
{
    public class TableStorageService
    {
        private readonly TableClient _tableClient;

        public TableStorageService(IConfiguration config)
        {
            string conn = config["AzureTableStorage:ConnectionString"];
            string tableName = config["AzureTableStorage:TableName"] ?? "Products";

            var service = new TableServiceClient(conn);
            _tableClient = service.GetTableClient(tableName);
            _tableClient.CreateIfNotExists();
        }

        public async Task AddProductAsync(Product product) =>
            await _tableClient.UpsertEntityAsync(product, TableUpdateMode.Replace);

        public async IAsyncEnumerable<Product> QueryProductsAsync(string partitionKey)
        {
            var q = _tableClient.QueryAsync<Product>(p => p.PartitionKey == partitionKey);
            await foreach (var item in q)
                yield return item;
        }
    }
}
