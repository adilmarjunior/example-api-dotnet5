using System;
using System.Collections.Generic;

namespace Catalog.Repositories.Interfaces
{
  public interface IRepository<Model>
  {
    IEnumerable<Model> GetAll();
    Model GetById(Guid id);
    Model Create(Model model);
    void Update(Model model);
    bool Delete(Model id);
  }
}