using ShoppingCart.Domain.Entities;
using ShoppingCart.Domain.Service;
namespace ShoppingCart.Domain.Test
{
    public class CartServiceTest
    {
        [Theory]
        [InlineData("Jean", "Jean", 100, 1)]
        [InlineData("T-Shirt", "Shirt", 50, 10)]
        public void CartService_AddProductToCart_ShouldContainAddedItem(string productName, string productDescription, decimal price, int quantityToCart)
        {
            //Arrange

            var cartService = new CartService();
            var cart = new Cart();
            var product = new Product
            {
                Id = 1,
                Description = productDescription,
                Name = productName,
                InStock = true,
                Price = price,
                Quantity = quantityToCart,
                QuantityLimit = quantityToCart
            };
            //Act

            cartService.AddProductToCart(cart, product, quantityToCart);

            //Asert
            var cartItem = cart.CartItems[0];

            Assert.Equal(product.Name, cartItem.ProductName);
            Assert.Equal(quantityToCart, cartItem.Quantity);
            Assert.Equal(product.Id, cartItem.ProductId);
            Assert.Equal(product.Price, cartItem.Price);
        }

        [Fact]
        public void CartService_CalculateTotalPrice_ShouldReturnCorrectTotalPrice()
        {
            //Arrange

            var cartService = new CartService();
            var cart = new Cart(new List<CartItem>
            {
                new CartItem
                {
                    ProductId= 1,
                    Price = 100,
                    Quantity = 1,
                    ProductName = "Jean"
                },
                new CartItem
                {
                    ProductId= 2,
                    Price = 70,
                    Quantity = 2,
                    ProductName = "T-Shirt"
                }
            });

            //Act

            var totalPrice = cartService.CalculateTotalPrice(cart);

            //Asert
            Assert.Equal(240M, totalPrice);
        }
    }
}