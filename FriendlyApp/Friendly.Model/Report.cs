
namespace Friendly.Model
{
    public class Report
    {
        public int Id { get; set; }
        public string AdditionalComment { get; set; }

        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
        public virtual Comment Comment { get; set; }
        public virtual ReportReason ReportReason { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Seen { get; set; }
    }
}
