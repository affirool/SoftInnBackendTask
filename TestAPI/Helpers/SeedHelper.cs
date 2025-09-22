using System.Text.Json;

namespace TestAPI.Helpers
{
    public static class SeedHelper
    {
        public static async Task<List<T>> LoadSeedData<T>(string path)
        {
            var json = await File.ReadAllTextAsync(path);

            return JsonSerializer.Deserialize<List<T>>(json) ?? [];
        }
    }
}
