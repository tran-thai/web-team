using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebTeam.Data;
using WebTeam.Data.Migrations;

namespace WebTeam.Models
{
    [Table("Comment")]

    public partial class Comment
    {
        public int CommentID { get; set; }

        public int? ArticleID { get; set; }

        public string? CommentContent { get; set; }

        public DateTime CommentDate { get; set; }

        [Required]
        public string? UserID { get; set; }
        /*[ForeignKey("UserID")]
        public IdentityUser? User { get; set; }*/

        [ForeignKey("ArticleID")]
        public virtual Article? Article { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser? User { get; set; }
        
    }
}
