using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staller.Core.Managers
{

    public class ManagerBase<TTableEntity> where TTableEntity : class, ITableEntity, new()
    {
        protected readonly string TableReference;
        protected CloudStorageAccount storageAccount;
        protected CloudTableClient tableClient;
        protected CloudTable table;

        public ManagerBase(string tableReference)
        {
            TableReference = tableReference;

            

            storageAccount = CloudStorageAccount.Parse(Configuration.ConnectionStrings.AzureStorage);
            tableClient = storageAccount.CreateCloudTableClient();
            table = tableClient.GetTableReference(TableReference);
        }


        protected void CreateTable()
        {
            // Create the table if it doesn't exist.
            table.CreateIfNotExistsAsync();
        }


        public async Task<TTableEntity> Get(string partitionKey, string rowKey)
        {
            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<TTableEntity>(partitionKey, rowKey);

            // Execute the retrieve operation.
            TableResult retrievedResult = await table.ExecuteAsync(retrieveOperation);

            return retrievedResult.Result as TTableEntity;
        }

        public async Task<IEnumerable<TTableEntity>> GetAll()
        {
            return await GetAll(string.Empty);

            //// Initialize a default TableQuery to retrieve all the entities in the table.
            //TableQuery<TTableEntity> tableQuery = new TableQuery<TTableEntity>();

            //// Initialize the continuation token to null to start from the beginning of the table.
            //TableContinuationToken continuationToken = null;

            //var resultset = new List<TTableEntity>();
            //do
            //{
            //    // Retrieve a segment (up to 1,000 entities).
            //    TableQuerySegment<TTableEntity> tableQueryResult = await table.ExecuteQuerySegmentedAsync(tableQuery, continuationToken);

            //    // Assign the new continuation token to tell the service where to
            //    // continue on the next iteration (or null if it has reached the end).
            //    continuationToken = tableQueryResult.ContinuationToken;

            //    resultset.AddRange(tableQueryResult.Results);

            //    // Loop until a null continuation token is received, indicating the end of the table.
            //} while (continuationToken != null);

            //return resultset;
        }


        public async Task<IEnumerable<TTableEntity>> GetAll(string partitionKey = null, string rowKey = null)
        {
            string partitionFilter = null;
            if (partitionKey != null)
                partitionFilter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey);

            string rowFilter = null;
            if (rowKey != null)
                rowFilter = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowKey);

            string filter = "";
            if (partitionFilter != null && rowFilter != null)
                filter = TableQuery.CombineFilters(partitionFilter, TableOperators.And, rowFilter);
            else
                filter = partitionFilter ?? rowFilter;

            return await GetAll(filter);
        }


        private async Task<IEnumerable<TTableEntity>> GetAll(string filter)
        {
            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<TTableEntity> tableQuery = new TableQuery<TTableEntity>().Where(filter);

            // Initialize the continuation token to null to start from the beginning of the table.
            TableContinuationToken continuationToken = null;

            var resultset = new List<TTableEntity>();
            do
            {
                // Retrieve a segment (up to 1,000 entities).
                TableQuerySegment<TTableEntity> tableQueryResult = await table.ExecuteQuerySegmentedAsync(tableQuery, continuationToken);

                // Assign the new continuation token to tell the service where to
                // continue on the next iteration (or null if it has reached the end).
                continuationToken = tableQueryResult.ContinuationToken;

                resultset.AddRange(tableQueryResult.Results);

                // Loop until a null continuation token is received, indicating the end of the table.
            } while (continuationToken != null);

            return resultset;
        }


        public async Task<TableResult> Save(TTableEntity entityToSave)
        {
            TableOperation saveOperation = TableOperation.InsertOrReplace(entityToSave);
            return await table.ExecuteAsync(saveOperation);
        }


        public async Task<TableResult> Delete(TTableEntity entityToDelete)
        {
            TableOperation deleteOperation = TableOperation.Delete(entityToDelete);
            return await table.ExecuteAsync(deleteOperation);
        }


    }
}
