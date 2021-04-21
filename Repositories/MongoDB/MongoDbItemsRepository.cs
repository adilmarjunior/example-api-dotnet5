using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Models;
using Catalog.Repositories.Interfaces;
using MongoDB.Driver;

namespace Catalog.Repositories.MongoDB
{
  public class MongoDbItemsRepository : IItemsRepository
  {
    private const string databaseName = "catalog";
    private const string collectionName = "items";
    private readonly IMongoCollection<Item> itemsCollection;
    private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

    public MongoDbItemsRepository(IMongoClient mongoClient)
    {
      IMongoDatabase database = mongoClient.GetDatabase(databaseName);
      itemsCollection = database.GetCollection<Item>(collectionName);
    }

    public async Task<Item> CreateAsync(Item model)
    {
      await itemsCollection.InsertOneAsync(model);
      return model;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
      var filter = filterBuilder.Eq(item => item.Id, id);
      var result = await itemsCollection.DeleteOneAsync(filter);
      return result.DeletedCount > 0;
    }

    public async Task<IEnumerable<Item>> GetAllAsync()
    {
      return await itemsCollection.Find<Item>(_ => true).ToListAsync();
    }

    public async Task<Item> GetByIdAsync(Guid id)
    {
      var filter = filterBuilder.Eq(item => item.Id, id);
      return await itemsCollection.Find(filter).SingleOrDefaultAsync();
      //return itemsCollection.Find<Item>(item => item.Id == id).FirstOrDefault();
    }

    public async Task UpdateAsync(Item model)
    {
      var filter = filterBuilder.Eq(existingItem => existingItem.Id, model.Id);
      await itemsCollection.ReplaceOneAsync(filter, model);
    }
  }
}