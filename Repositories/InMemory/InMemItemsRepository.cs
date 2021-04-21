using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Models;
using Catalog.Repositories.Interfaces;

namespace Catalog.Repositories.InMemory
{
  public class InMemItemsRepository : IItemsRepository
  {
    private readonly List<Item> items = new()
    {

      new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
      new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
      new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow },
      new Item { Id = Guid.NewGuid(), Name = "Boots", Price = 10, CreatedDate = DateTimeOffset.UtcNow },
    };

    public Item Create(Item model)
    {
      items.Add(model);
      return model;
    }

    public bool Delete(Guid id)
    {
      var index = this.items.FindIndex(existingItem => existingItem.Id == id);
      this.items.RemoveAt(index);
      return true;
    }

    public IEnumerable<Item> GetAll()
    {
      return items;
    }

    public Item GetById(Guid id)
    {
      return items.Where(item => item.Id == id).SingleOrDefault();
    }

    public void Update(Item item)
    {
      var index = this.items.FindIndex(existingItem => existingItem.Id == item.Id);

      if(index == -1) return;

      this.items[index] = item;
    }
  }
}