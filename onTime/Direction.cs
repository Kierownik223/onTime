using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onTime
{
    public class AltStopPoint
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public object City { get; set; }
        public object Distance { get; set; }
        public bool Virtual { get; set; }
        public bool OnRequest { get; set; }
        public bool GettingOut { get; set; }
        public List<StopPointValidity> StopPointValidity { get; set; }
    }

    public class Connection
    {
        public bool Main { get; set; }
        public List<Node> Nodes { get; set; }
        public string FromSymbol { get; set; }
        public string ToSymbol { get; set; }
        public int ValidFrom { get; set; }
        public object ValidUntil { get; set; }
    }

    public class Node
    {
        public int OrderNo { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }

    public class Direction
    {
        public object Destination { get; set; }
        public object AltDestination { get; set; }
        public int MainVariantLoid { get; set; }
        public int AltVariantLoid { get; set; }
        public List<Connection> Connections { get; set; }
        public List<StopPoint> StopPoints { get; set; }
        public List<AltStopPoint> AltStopPoints { get; set; }
        public bool HasAnotherDirection { get; set; }
        public static async Task<Direction> GetDirection(string lineId, bool thereDirection, string url = "https://rozklady.bielsko.pl/getDirection.json")
        {
            if (lineId == null)
            {
                System.Windows.MessageBox.Show("Line ID cannot be empty!");
                return null;
            }
            var response = await MainPage.Current.HttpClient.GetAsync($"{url}?lineId={lineId}&thereDirection={thereDirection}");
            if (!response.IsSuccessStatusCode)
                return null;

            string responseText = await response.Content.ReadAsStringAsync();
            Direction direction = JsonConvert.DeserializeObject<Direction>(responseText);

            int index = 0;
            foreach (StopPoint stop in direction.StopPoints)
                stop.FeatheringIndex = index++;

            return direction;
        }
    }

    public class StopPoint
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public object City { get; set; }
        public object Distance { get; set; }
        public bool Virtual { get; set; }
        public bool OnRequest { get; set; }
        public bool GettingOut { get; set; }
        public List<StopPointValidity> StopPointValidity { get; set; }
        public object FeatheringIndex { get; set; }
    }

    public class StopPointValidity
    {
        public object From { get; set; }
        public object Until { get; set; }
    }


}
