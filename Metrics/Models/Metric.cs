using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metrics.Models
{
    public class Metric
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public string Unit { get; set; } = "";
        public int Precision { get; set; } = 0;
        public decimal Minimum { get; set; } = 0;
        public decimal Maximum { get; set; } = 0;

        /// <summary>
        /// for testing
        /// </summary>
        /// <returns>random decimal value following min/max/precision limits, with the unit suffix</returns>
        public string GenerateTestValue()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            return Math.Round((decimal)rnd.NextDouble() * (Maximum - Minimum) + Minimum, Precision) + " " + Unit;
        }

        /// <summary>
        /// for testing. final version would include a dapper call to a database for this list
        /// </summary>
        /// <returns>A static list of metrics</returns>
        public static IEnumerable<Metric> GetAvailableTest()
        {
            List<Metric> available = new List<Metric>();
            available.Add(new Metric() { Id = 1, Name = "Document Complete", Unit = "ms", Precision = 0, Minimum = 500, Maximum = 5000 });
            available.Add(new Metric() { Id = 2, Name = "Connect", Unit = "ms", Precision = 0, Minimum = 1, Maximum = 100 });
            available.Add(new Metric() { Id = 3, Name = "DNS", Unit = "ms", Precision = 0, Minimum = 5, Maximum = 300 });
            available.Add(new Metric() { Id = 4, Name = "Total Bytes", Unit = "kb", Precision = 0, Minimum = 250, Maximum = 1440 });
            return available;
        }
    }
}
