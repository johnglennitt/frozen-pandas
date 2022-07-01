using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    internal class FunctionalCart
    {
        private ImmutableStack<ItemInCartEntity> items;

        private FunctionalCart(ImmutableStack<ItemInCartEntity> items)
        {
            this.items = items;
        }

        public static FunctionalCart NewCart()
        {
            return new FunctionalCart(ImmutableStack<ItemInCartEntity>.Empty);
        }

        public static FunctionalCart AddItemToCart(FunctionalCart cart, int itemId, uint quantity)
        {
            // get a new stack with the item
            var newStack = cart.items.Push(new ItemInCartEntity(itemId, (int)quantity));

            return new FunctionalCart(newStack);
        }

        public static FunctionalCart RemoveItemFromCartEntirely(FunctionalCart cart, int itemId)
        {
            // calculate how many items of this type are in the cart
            var currentCount = Math.Max(
                0,
                cart.items.Where(i => i.Id == itemId).Sum(i => i.Quantity)
            );

            // get a new stack with the item
            var cancellationItem = new ItemInCartEntity(itemId, currentCount * -1);
            var newStack = cart.items.Push(cancellationItem);

            return new FunctionalCart(newStack);
        }

        public static FunctionalCart RemoveItemFromCartByQuantity(FunctionalCart cart, int itemId, uint quantityToRemove)
        {
            // calculate how many items of this type are in the cart
            var removeMe = Math.Min(
                (int)quantityToRemove,
                cart.items.Where(i => i.Id == itemId).Sum(i => i.Quantity) // don't remove more items than are present in the cart
            );

            // get a new stack with the new entry to remove the items
            var cancellationItem = new ItemInCartEntity(itemId, removeMe * -1);
            var newStack = cart.items.Push(cancellationItem);

            return new FunctionalCart(newStack);
        }

        public static ImmutableArray<Tuple<int, int>> GenerateItemQuantityMatrix(FunctionalCart cart)
        {
            return cart.items
                .GroupBy(i => i.Id)
                .Select(g => new Tuple<int, int>(g.Key, g.Sum(i => i.Quantity)))
                .ToImmutableArray();
        }
    }
}
