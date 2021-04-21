using System;
using System.Collections.Generic;
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

    public Item Create(Item model)
    {
      itemsCollection.InsertOne(model);
      return model;
    }

    public bool Delete(Guid id)
    {
      var filter = filterBuilder.Eq(item => item.Id, id);
      return itemsCollection.DeleteOne(filter).DeletedCount > 0;
    }

    public IEnumerable<Item> GetAll()
    {
      return itemsCollection.Find<Item>(_ => true).ToList();
    }

    public Item GetById(Guid id)
    {
      var filter = filterBuilder.Eq(item => item.Id, id);
      return itemsCollection.Find(filter).SingleOrDefault();
      //return itemsCollection.Find<Item>(item => item.Id == id).FirstOrDefault();
    }

    public void Update(Item model)
    {
      var filter = filterBuilder.Eq(existingItem => existingItem.Id, model.Id);
      itemsCollection.ReplaceOne(filter, model);
    }
  }
}