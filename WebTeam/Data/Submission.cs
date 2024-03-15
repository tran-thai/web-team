using System.ComponentModel.DataAnnotations.Schema;
using WebTeam.Data.Migrations;

namespace WebTeam.Data
{
    [Table("Submission")]
    public partial class Submission
    {
        public int SubmissionID { get; set; }

        public string? StudentID { get; set; }

        public DateTime SubmissionDate { get; set; }

        public string Status { get; set; }

        public string? CommentSubContent { get; set; }

        public string FileSubName { get; set; }

        public int? FeedBackID { get; set; }

        [ForeignKey("StudentID")]
        public virtual ApplicationUser? Student { get; set; }

        [ForeignKey("FeedBackID")]
        public virtual FeedBack? FeedBack { get; set; }
    }
}
