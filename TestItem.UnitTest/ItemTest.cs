using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TodoItem2.Model.Entities;
using TodoItem2.Model.Repositories;

namespace TestItem.UnitTest
{
    [TestClass]
    public class ItemTest
    {
        Mock<IItemRepository> itemRepository = new Mock<IItemRepository>();
        [TestMethod]
        public void GetItems_ReturnsEnumerableItem()
        {
            // Act
            var result = itemRepository.Object.GetAll();
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetItemBy_ItemId_ReturnsSpecifiedItem()
        {
            const int itemId = 1;
            var result = itemRepository.Object.GetById(itemId);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void AddItem_ReturnsNewItem()
        {
            var item = GetItem(false,true);
            var result = itemRepository.Object.Create(item);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void UpdateItem_ReturnsItemUpdated()
        {
            var item = GetItem(true,false);
            var result = itemRepository.Object.Update(item);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void DeleteItem_ReturnsItemToDelete()
        {
            const int itemId = 1;
            var result = itemRepository.Object.Delete(itemId);
            Assert.IsNotNull(result);
        }

        private Item GetItem(bool isUpdated, bool newItem)
        {
            Item item = new Item();
            if(!newItem) item.Id = 1;
            item.Name = "Santiago";
            item.Description = "Monumento y mujeres lindas";
            item.Deleted = false;
            item.DetetedDate = System.DateTime.Now;
            if (isUpdated == true) item.UpdateDate = System.DateTime.Now;
            else item.UpdateDate = null;
            return item;
        }

    }
}
