using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Repositories.Interfaces
{
  public interface IRepository<Model>
  {
    Task<Model> GetByIdAsync(Guid id);
    Task<IEnumerable<Model>> GetAllAsync();
    Task<Model> CreateAsync(Model model);
    Task UpdateAsync(Model model);
    Task<bool> DeleteAsync(Guid id);
  }
}