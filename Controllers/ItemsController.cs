using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.DTOs.Item;
using Catalog.Models;
using Catalog.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
  [ApiController]
  [Route("items")]
  public class ItemsController : ControllerBase
  {
    private readonly IItemsRepository itemsRepository;

    public ItemsController(IItemsRepository itemsRepository)
    {
      this.itemsRepository = itemsRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<ItemDto>> GetItems()
    {
      var items =  await this.itemsRepository.GetAllAsync();

      return items.Select(item => new ItemDto(item));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetItem(Guid id)
    {
      var item = await this.itemsRepository.GetByIdAsync(id);

      if(item is null) return NotFound();

      return Ok(new ItemDto(item)); 
    }

    [HttpPost]
    public async Task<ActionResult<ItemDto>> CreateItem(CreateItemDto itemDto)
    {
      Item itemToInsert = new(){
        Id = Guid.NewGuid(),
        Name = itemDto.Name,
        Price = itemDto.Price,
        CreatedDate = DateTimeOffset.UtcNow
      };

      var item = await this.itemsRepository.CreateAsync(itemToInsert);

      return CreatedAtAction(nameof(GetItem), new { id = item.Id },  new ItemDto(item));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateItem(Guid id, UpdateItemDto itemDto)
    {
      var existingItem = await this.itemsRepository.GetByIdAsync(id);

      if(existingItem is null) return NotFound();

      var updatedItem = existingItem with {
        Name = itemDto.Name,
        Price = itemDto.Price
      };

      await this.itemsRepository.UpdateAsync(updatedItem);

      return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteItem(Guid id)
    {
      var existingItem = this.itemsRepository.GetByIdAsync(id);

      if(existingItem is null) return NotFound();

      this.itemsRepository.DeleteAsync(id);

      return NoContent();
    }
  }
}