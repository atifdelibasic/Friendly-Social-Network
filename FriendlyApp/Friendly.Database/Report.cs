namespace Friendly.Database
{
    public class Report : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public int ReportReasonId { get; set; }
        public string AdditionalComment { get; set; }
        public bool Seen { get; set; }

        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
        public virtual Comment Comment { get; set; }
        public virtual ReportReason ReportReason { get; set; }
    }
}
