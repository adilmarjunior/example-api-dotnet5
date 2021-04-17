using System;
using System.Collections.Generic;
using System.Linq;
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
    public IEnumerable<ItemDto> GetItems()
    {
      return this.itemsRepository.GetAll()
        .Select(item => new ItemDto(item));
    }

    [HttpGet("{id}")]
    public ActionResult<ItemDto> GetItem(Guid id)
    {
      var item = this.itemsRepository.GetById(id);

      if(item is null) return NotFound();

      return Ok(new ItemDto(item)); 
    }

    [HttpPost]
    public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
    {
      Item itemToInsert = new(){
        Id = Guid.NewGuid(),
        Name = itemDto.Name,
        Price = itemDto.Price,
        CreatedDate = DateTimeOffset.UtcNow
      };

      var item = this.itemsRepository.Create(itemToInsert);

      return CreatedAtAction(nameof(GetItem), new { id = item.Id },  new ItemDto(item));
    }

    [HttpPut("{id}")]
    public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
    {
      var existingItem = this.itemsRepository.GetById(id);

      if(existingItem is null) return NotFound();

      var updatedItem = existingItem with {
        Name = itemDto.Name,
        Price = itemDto.Price
      };

      this.itemsRepository.Update(updatedItem);

      return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteItem(Guid id)
    {
      var existingItem = this.itemsRepository.GetById(id);

      if(existingItem is null) return NotFound();

      this.itemsRepository.Delete(existingItem);

      return NoContent();
    }
  }
}