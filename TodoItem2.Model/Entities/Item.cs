using System;

namespace TodoItem2.Model.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DetetedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
