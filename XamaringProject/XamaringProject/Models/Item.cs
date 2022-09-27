using System;

namespace XamaringProject.Models
{
    public class Item
    {
        //[Key]//make sure ID is the unique key
        public int id { get; set; }
        public string customerName { get; set; }
        public int productID { get; set; }
        public decimal newPrice { get; set; }
    }
}