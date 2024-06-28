using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DynamicDashboardAspPostgres.Models
{
    public class Chart
    {
        [Key]
        public int Id { get; set; }
        public int PositionRow { get; set; }
        public int PositionCol { get; set; }
        public string ChartType { get; set; }
        public int SizeRows { get; set; }
        public int SizeCols { get; set; }

        public int DataId { get; set; }
        [ForeignKey("DataId")]
        public ChartData Data { get; set; }
    }
}
