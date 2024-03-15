using System.ComponentModel.DataAnnotations.Schema;
using WebTeam.Data.Migrations;
using WebTeam.Models;

namespace WebTeam.Data
{
    [Table("Like")]
    public partial class Like
    {
        public int LikeID { get; set; }

        public int? ArticleID { get; set; }

        public string? UserID { get; set; }

        [ForeignKey("ArticleID")]
        public virtual Article? Article { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser? User { get; set; }
    }
}
