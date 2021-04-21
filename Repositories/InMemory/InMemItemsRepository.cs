using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public async Task<Item> CreateAsync(Item model)
    {
      items.Add(model);

      return await Task<Item>.FromResult(model);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
      var index = this.items.FindIndex(existingItem => existingItem.Id == id);
      this.items.RemoveAt(index);

      return await Task.FromResult(true);
    }

    public async Task<IEnumerable<Item>> GetAllAsync()
    {
      return await Task.FromResult(items);
    }

    public async Task<Item> GetByIdAsync(Guid id)
    {
      var item = items.Where(item => item.Id == id).SingleOrDefault();
      return await Task.FromResult(item);
    }

    public async Task UpdateAsync(Item item)
    {
      var index = this.items.FindIndex(existingItem => existingItem.Id == item.Id);

      if(index >= 0)
      {
        this.items[index] = item;
      }

      await Task.CompletedTask;
    }
  }
}