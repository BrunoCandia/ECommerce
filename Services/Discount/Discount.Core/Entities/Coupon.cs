﻿namespace Discount.Core.Entities
{
    public class Coupon
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public decimal AmountOff { get; set; }
    }
}
