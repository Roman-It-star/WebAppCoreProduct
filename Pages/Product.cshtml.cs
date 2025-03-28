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
            MessageRezult = "Для товара можно определить скидку";
        }

        public void OnPost(string name, decimal? price)
        {
            Product = new Product();
            if (price == null || price < 0 || string.IsNullOrEmpty(name))
            {
                MessageRezult = "Переданы некорректные данные. Повторите ввод";
                return;
            }
            try
            {
                var result = _discountService.CalculateStandardDiscount(price.Value);
                MessageRezult = $"Для товара {name} с ценой {price} скидка получится {result}";
                Product.Price = price;
                Product.Name = name;

            }
            catch (Exception ex)
            {
                MessageRezult = $"Ошибка: {ex.Message}";
            }
        }

        public void OnPostDiscont(string name, decimal? price, double discount)
        {
            Product = new Product();
            if (price == null || price < 0 || string.IsNullOrEmpty(name) || discount < 0)
            {
                MessageRezult = "Переданы некорректные данные. Повторите ввод";
                return;
            }

            try
            {
                // Расчет скидки через сервис
                var discountAmount = _discountService.CalculateDiscount(price.Value, discount);
                var finalPrice = price.Value - discountAmount;

                // Формирование результата
                MessageRezult = $"Товар: {name}\n" +
                               $"Цена: {price} руб.\n" +
                               $"Скидка: {discount}% ({discountAmount} руб.)\n" +
                               $"Итоговая цена: {finalPrice} руб.";

                Product.Name = name;
                Product.Price = price.Value;
            }
            catch (Exception ex)
            {
                MessageRezult = $"Ошибка: {ex.Message}";
            }

        }

        public void OnPostDoubleDiscount(string name, decimal? price)
        {
            Product = new Product();
            if (price == null || price < 0 || string.IsNullOrEmpty(name))
            {
                MessageRezult = "Переданы некорректные данные. Повторите ввод";
                return;
            }

            try
            {

                // Двойная стандартная скидка
                var result = _discountService.CalculateStandardDiscount(price.Value) * 2;
                MessageRezult = $"Для товара {name} с ценой {price} двойная скидка получится {result}";
                Product.Price = price;
                Product.Name = name;
            }
            catch (Exception ex)
            {
                MessageRezult = $"Ошибка: {ex.Message}";
            }
        }

    }
}
