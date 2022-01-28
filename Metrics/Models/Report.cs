using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Metrics.Models
{
    public class Report
    {

        public IEnumerable<Metric> SelectedMetrics { get; set; } = new List<Metric>();

        /// <summary>
        /// for testing.
        /// </summary>
        /// <param name="rowCount">optional, default 5</param>
        /// <returns>Table containing ID + selected metrics</returns>
        public DataTable GetDataTest(int rowCount = 5)
        {
            DataTable Data = new DataTable();

            Data.Columns.Add("Id");

            foreach (Metric metric in SelectedMetrics)
            {
                Data.Columns.Add(metric.Name);
            }
            for (int r = 0; r < rowCount; r++)
            {
                DataRow row = Data.NewRow();
                row["Id"] = r + 1;
                foreach (Metric metric in SelectedMetrics)
                {
                    row[metric.Name] = metric.GenerateTestValue();
                }
                Data.Rows.Add(row);
            }

            return Data;
        }

    }
}
