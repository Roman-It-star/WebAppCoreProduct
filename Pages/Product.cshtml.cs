using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.CompilerServices;
using WebAppCoreProduct.Interface;
using WebAppCoreProduct.Models;

namespace WebAppCoreProduct.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IDiscountService _discountService;

        public ProductModel(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        public Product Product { get; set; }
        public string? MessageRezult { get; private set; }

        public void OnGet()
        {
            MessageRezult = "��� ������ ����� ���������� ������";
        }

        public void OnPost(string name, decimal? price)
        {
            Product = new Product();
            if (price == null || price < 0 || string.IsNullOrEmpty(name))
            {
                MessageRezult = "�������� ������������ ������. ��������� ����";
                return;
            }
            try
            {
                var result = _discountService.CalculateStandardDiscount(price.Value);
                MessageRezult = $"��� ������ {name} � ����� {price} ������ ��������� {result}";
                Product.Price = price;
                Product.Name = name;

            }
            catch (Exception ex)
            {
                MessageRezult = $"������: {ex.Message}";
            }
        }

        public void OnPostDiscont(string name, decimal? price, double discount)
        {
            Product = new Product();
            if (price == null || price < 0 || string.IsNullOrEmpty(name) || discount < 0)
            {
                MessageRezult = "�������� ������������ ������. ��������� ����";
                return;
            }

            try
            {
                // ������ ������ ����� ������
                var discountAmount = _discountService.CalculateDiscount(price.Value, discount);
                var finalPrice = price.Value - discountAmount;

                // ������������ ����������
                MessageRezult = $"�����: {name}\n" +
                               $"����: {price} ���.\n" +
                               $"������: {discount}% ({discountAmount} ���.)\n" +
                               $"�������� ����: {finalPrice} ���.";

                Product.Name = name;
                Product.Price = price.Value;
            }
            catch (Exception ex)
            {
                MessageRezult = $"������: {ex.Message}";
            }

        }

        public void OnPostDoubleDiscount(string name, decimal? price)
        {
            Product = new Product();
            if (price == null || price < 0 || string.IsNullOrEmpty(name))
            {
                MessageRezult = "�������� ������������ ������. ��������� ����";
                return;
            }

            try
            {

                // ������� ����������� ������
                var result = _discountService.CalculateStandardDiscount(price.Value) * 2;
                MessageRezult = $"��� ������ {name} � ����� {price} ������� ������ ��������� {result}";
                Product.Price = price;
                Product.Name = name;
            }
            catch (Exception ex)
            {
                MessageRezult = $"������: {ex.Message}";
            }
        }

    }
}
