using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace KairoSync
{
    public class jsonreader
    {
        public static List<Ülke> ReadCountries(string filePath)
        {
            try
            {
                // Dosyayı okuyup JSON olarak deserialize ediyoruz
                string jsonString = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<Ülke>>(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON dosyası okunurken bir hata oluştu: {ex.Message}");
                return new List<Ülke>();
            }
        }
    }
}
