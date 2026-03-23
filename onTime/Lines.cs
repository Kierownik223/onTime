using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onTime
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Line
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool NightLine { get; set; }
        public string LineType { get; set; }
        public List<LineValidity> LineValidity { get; set; }
    }

    public class LineValidity
    {
        public int From { get; set; }
        public object Until { get; set; }
    }

    public class Lines
    {
        public List<Line> lines { get; set; }
        public static async Task<List<Line>> GetLines(string url = "https://rozklady.bielsko.pl/getLines.json?")
        {
            var response = await MainPage.Current.HttpClient.GetAsync(url);            
            if (!response.IsSuccessStatusCode)
                return null;

            string responseText = await response.Content.ReadAsStringAsync();
            Lines lines = JsonConvert.DeserializeObject<Lines>(responseText);

            return lines.lines;
        }
    }
}
