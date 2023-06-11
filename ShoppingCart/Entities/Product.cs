﻿namespace ShoppingCart.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
        public int Quantity { get; set; }
        public int QuantityLimit { get; set; }
    }
}
