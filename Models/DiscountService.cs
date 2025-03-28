using WebAppCoreProduct.Interface;

namespace WebAppCoreProduct.Models
{
    public class DiscountService : IDiscountService
    {
        private const decimal StandardDiscountRate = 0.18m;

        public decimal CalculateDiscount(decimal price, double discountPercent)
        {
            if (price < 0 || discountPercent < 0)
                throw new ArgumentException("Цена и скидка не могут быть отрицательными");

            return price * (decimal)(discountPercent / 100);
        }

        public decimal CalculateStandardDiscount(decimal price)
        {
            if (price < 0)
                throw new ArgumentException("Цена не может быть отрицательной");

            return price * StandardDiscountRate;
        }
    }
}
