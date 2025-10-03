using ICE_TASK_2.Models;
using ICE_TASK_2.Service;
using Microsoft.AspNetCore.Mvc;

namespace ICE_TASK_2.Controllers
{
    public class ProductController : Controller
    {
        private readonly TableStorageService _table;

        public ProductController(TableStorageService table)
        {
            _table = table;
        }

        public async Task<IActionResult> Index()
        {
            var list = new List<Product>();
            await foreach (var p in _table.QueryProductsAsync("Products"))
            {
                list.Add(p);
            }
            return View(list);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(
            string name,
            int quantity,
            decimal price,
            bool clearance = false
        )
        {
            var product = new Product
            {
                RowKey = Guid.NewGuid().ToString(),
                Name = name,
                Quantity = quantity,
                Price = price,
                Clearance = clearance,
            };

            await _table.AddProductAsync(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
