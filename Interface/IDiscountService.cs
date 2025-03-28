namespace WebAppCoreProduct.Interface
{
    public interface IDiscountService
    {
        decimal CalculateDiscount(decimal price, double discountPercent);
        decimal CalculateStandardDiscount(decimal price);
    }
}
