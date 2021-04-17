using System;

namespace Catalog.DTOs.Item
{
  public class ItemDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTimeOffset CreatedDate { get; set; }

    public ItemDto(Catalog.Models.Item item)
    {
      this.Id = item.Id;
      this.Name = item.Name;
      this.Price = item.Price;
      this.CreatedDate = item.CreatedDate;
    }
  }
}