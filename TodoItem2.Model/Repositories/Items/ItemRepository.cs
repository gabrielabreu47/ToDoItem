using System;
using System.Collections.Generic;
using System.Linq;
using TodoItem2.Model.Entities;

namespace TodoItem2.Model.Repositories
{
    public class ItemRepository : IItemRepository
    {
        public readonly ItemContext _itemContext;
        public ItemRepository(ItemContext itemContext)
        {
            _itemContext = itemContext;
        }
        public Item Create(Item item)
        {
           var itemResult = _itemContext.Items.Add(item);
            _itemContext.SaveChanges();
            return itemResult.Entity;
        }

        public Item Delete(int id)
        {
            var itemToDelete = GetAll().FirstOrDefault(x => x.Id == id);
            itemToDelete.Deleted = true;
            itemToDelete.DetetedDate = DateTime.Now;
            _itemContext.SaveChanges();
            return itemToDelete;
        }

        public IEnumerable<Item> GetAll()
        {
            var items = _itemContext.Items.Where(x => x.Deleted == false);
            return items;
        }

        public Item GetById(int id)
        {
            var item = GetAll().FirstOrDefault(x => x.Id == id);
            return item;
        }

        public Item Update(Item item)
        {
            item.UpdateDate = DateTime.Now;
            _itemContext.Items.Update(item);
            _itemContext.SaveChanges();
            return item;
        }
    }
}
