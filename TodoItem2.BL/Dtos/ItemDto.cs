using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoItem2.BL.Dtos
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DetetedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string FullName { get; set; }
    }
}
