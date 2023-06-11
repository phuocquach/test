using ShoppingCart.Domain.Exceptions;

namespace ShoppingCart.Domain.Entities
{
    public class Cart
    {
        private readonly List<CartItem> _cartItems;

        public IReadOnlyList<CartItem> CartItems => _cartItems;

        public Cart(IEnumerable<CartItem> cartItems)
        {
            _cartItems = new List<CartItem>(cartItems);
        }
        public Cart()
        {
            _cartItems = new List<CartItem>();
        }
        internal void RemoveItem(Product product)
        {
            var item = _cartItems.FirstOrDefault(x => x.ProductId == product.Id);
            _cartItems.Remove(item);
        }

        internal void AddItem(Product product, int quantity)
        {
            if (!IsProductAvailable(product, quantity))
            {
                throw new DomainException("Product is not available in the desired quantity.");
            }

            if (ExceedsQuantityLimit(product, quantity))
            {
                throw new DomainException("Quantity exceeds the limit for this product.");
            }

            var newItem = new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = quantity
            };

            _cartItems.Add(newItem);
        }

        private static bool IsProductAvailable(Product product, int quantity)
        {
            return product != null && product.InStock && product.Quantity >= quantity;
        }

        private static bool ExceedsQuantityLimit(Product product, int quantity)
        {
            return quantity > product.QuantityLimit;
        }

        internal decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;
            foreach (var item in _cartItems)
            {
                totalPrice += item.Price * item.Quantity;
            }
            return totalPrice;
        }
    }
}
