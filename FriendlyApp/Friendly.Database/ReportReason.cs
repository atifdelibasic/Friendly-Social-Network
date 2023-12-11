using System.ComponentModel.DataAnnotations;

namespace Friendly.Database
{
    public class ReportReason
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
