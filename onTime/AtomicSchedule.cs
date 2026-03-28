using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onTime
{
    public class ScheduleLine
    {
        public string LineName { get; set; }
        public bool IsGettingOutOnly { get; set; }
        public string Destination { get; set; }
        public int MainVariantId { get; set; }
        public List<Departure> Departures { get; set; }
        public List<LineValidity> LineValidity { get; set; }
    }

    public class AllLinesInActiveStrap
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool NightLine { get; set; }
        public object LineType { get; set; }
        public List<object> LineValidity { get; set; }
    }

    public class Departure
    {
        public int ScheduledDepartureSec { get; set; }
        public string Brigade { get; set; }
        public int CourseId { get; set; }
        public int VariantId { get; set; }
        public object Operator { get; set; }
        public string TransportType { get; set; }
        public string ExpectedVehicleType { get; set; }
        public int OrderInCourse { get; set; }
        public string OptionalDirection { get; set; }
        public string Letter { get; set; }
        public bool Visible { get; set; }
        public object WorkaroundId { get; set; }
        public object OverloadedId { get; set; }
        public int CourseStartSec { get; set; }
        public List<object> MultipleLegends { get; set; }
    }

    public class AtomicSchedule
    {
        public string DayTypeSymbol { get; set; }
        public List<object> GettingOutVariants { get; set; }
        public Dictionary<string, ScheduleLine> LineSchedules { get; set; }
        public List<AllLinesInActiveStrap> AllLinesInActiveStraps { get; set; }
        public object ScheduleDate { get; set; }
        public bool AutoLettersAssignment { get; set; }
        public bool MultipleLettersAssignment { get; set; }
        public static async Task<AtomicSchedule> GetAtomicSchedule(string symbol, DateTime date, string url = "https://rozklady.bielsko.pl/getAtomicSchedule.json")
        {
            if (symbol == null)
            {
                System.Windows.MessageBox.Show("Stop symbol cannot be empty!");
                return null;
            }

            long dayMilis = (long)(new DateTime(date.Year, date.Month, date.Day) -
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .TotalMilliseconds;

            var response = await MainPage.Current.HttpClient.GetAsync($"{url}?symbol={symbol}&date={dayMilis}");
            if (!response.IsSuccessStatusCode)
                return null;

            string responseText = await response.Content.ReadAsStringAsync();
            AtomicSchedule atomicSchedule = JsonConvert.DeserializeObject<AtomicSchedule>(responseText);

            return atomicSchedule;
        }
    }
}
