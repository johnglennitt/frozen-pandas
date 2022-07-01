using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    internal class ItemInCartEntity
    {
        public ItemInCartEntity(int id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }

        public int Id { get; set; }

        public int Quantity { get; set; }
    }
}
