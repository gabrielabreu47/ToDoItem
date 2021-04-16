using System;
using System.Collections.Generic;
using System.Text;
using TodoItem2.Model.Entities;
namespace TodoItem2.Model.Repositories
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetAll();
        Item Create(Item item);
        Item GetById(int id);
        Item Update(Item item);
        Item Delete(int id);
    }
}
