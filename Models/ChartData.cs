using System.ComponentModel.DataAnnotations;
using System.Data;

namespace DynamicDashboardAspPostgres.Models
{
    public class ChartData
    {

        [Key]
        public int Id { get; set; }
        public List<string> Labels { get; set; }
        public List<DataSet> Datasets { get; set; }
    }
}
