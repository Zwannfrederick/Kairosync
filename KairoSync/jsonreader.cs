using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace KairoSync
{
    public class Jsonreader
    {
        public static List<Ülke> ReadCountries(string filePath)
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                var countries = JsonSerializer.Deserialize<List<Ülke>>(jsonString);

                // Deserialize işlemi sonucu null dönerse boş liste döndür
                return countries ?? new List<Ülke>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON dosyası okunurken bir hata oluştu: {ex.Message}");
                return new List<Ülke>();
            }
        }
    }
}
