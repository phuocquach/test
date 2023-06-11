using ShoppingCart.Domain.Entities;
using ShoppingCart.Domain.Exceptions;

namespace ShoppingCart.Domain.Service
{
    public interface ICartService
    {
        public void AddProductToCart(Cart cart, Product product, int quantity);
        public decimal CalculateTotalPrice(Cart cart);
    }

    public class CartService : ICartService
    {
        public void AddProductToCart(Cart cart, Product product, int quantity)
        {
            if (cart == null)
            {
                throw new DomainException("Cart is not available");
            };
            if (product == null)
            {
                throw new DomainException("Product is not available");
            }
            if (quantity <= 0)
            {
                throw new DomainException("Quantity should be greater than 0");
            }

            cart.AddItem(product, quantity);
        }

        public decimal CalculateTotalPrice(Cart cart)
        {
            if (cart == null)
            {
                throw new DomainException("Cart is not available");
            };

            var totalPrice = cart.CalculateTotalPrice();

            return totalPrice;
        }

    }
}
