using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TeknoMarket.Models;

namespace TeknoMarket.Controllers
{
    public class HomeController : Controller
    {
        // Geçen yaptığın projedeki ürün listesini C# yapısına taşıdık
        private static readonly List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "iPhone 15 Pro", Category = "telefon", Price = 75000, Description = "A17 Pro çip, titanyum tasarım ve muhteşem kamera.", IsInStock = true },
            new Product { Id = 2, Name = "Asus ROG Strix", Category = "bilgisayar", Price = 45000, Description = "RTX 4060 ekran kartı ile canavar performans.", IsInStock = true },
            new Product { Id = 3, Name = "Samsung Galaxy S24", Category = "telefon", Price = 55000, Description = "Yapay zeka özellikleriyle donatılmış ekran deneyimi.", IsInStock = false },
            new Product { Id = 4, Name = "MacBook Air M3", Category = "bilgisayar", Price = 42000, Description = "İnce, hafif ve gün boyu süren pil ömrü.", IsInStock = true }
        };

        // Ön yüzden gönderilen form verilerini buradan yakalıyoruz
        public IActionResult Index(string searchInput, string categorySelect, string stockRadio)
        {
            var sonuclar = _products.AsQueryable();

            // Arama kutusuna yazı yazıldıysa süz
            if (!string.IsNullOrEmpty(searchInput))
            {
                sonuclar = sonuclar.Where(p => p.Name.Contains(searchInput, System.StringComparison.OrdinalIgnoreCase));
            }

            // Kategori seçildiyse süz
            if (!string.IsNullOrEmpty(categorySelect) && categorySelect != "all")
            {
                sonuclar = sonuclar.Where(p => p.Category == categorySelect);
            }

            // Sadece stoktakiler seçildiyse süz
            if (stockRadio == "true")
            {
                sonuclar = sonuclar.Where(p => p.IsInStock == true);
            }

            return View(sonuclar.ToList());
        }
    }
}