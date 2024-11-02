using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public class FileHelper
{
    private static readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "products.json");

    public static async Task<List<Product>> ReadProductsAsync()
    {
        if (!File.Exists(filePath))
        {
            await File.WriteAllTextAsync(filePath, "[]");
        }

        var jsonData = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Product>>(jsonData) ?? new List<Product>();
    }

    public static async Task WriteProductsAsync(List<Product> products)
    {
        var jsonData = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, jsonData);
    }
}
