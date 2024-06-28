using System.ComponentModel.DataAnnotations;

namespace DynamicDashboardAspPostgres.Models
{
    public class DataSet
    {
        [Key]
        public int Id { get; set; }
        public string Labels { get; set; }
        public List<int> Data { get; set; }
        public List<string> BackgroundColor { get; set; }
    }
}
