using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ProductController : Controller
{
    // Retrieve all products from the JSON file
    [HttpGet]
    public async Task<JsonResult> GetProducts()
    {
        var products = await FileHelper.ReadProductsAsync();
        return Json(products);
    }

    // Add a new product and save to the JSON file
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        var products = await FileHelper.ReadProductsAsync();
        product.Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
        products.Add(product);
        await FileHelper.WriteProductsAsync(products);
        return Json(product);
    }

    // Update an existing product and save to the JSON file
    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] Product product)
    {
        var products = await FileHelper.ReadProductsAsync();
        var existingProduct = products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;
            await FileHelper.WriteProductsAsync(products);
            return Json(existingProduct);
        }
        return NotFound();
    }

    // Delete a product and save the updated list to the JSON file
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var products = await FileHelper.ReadProductsAsync();
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            products.Remove(product);
            await FileHelper.WriteProductsAsync(products);
            return Ok();
        }
        return NotFound();
    }
}
