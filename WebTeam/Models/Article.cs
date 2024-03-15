﻿using System.ComponentModel.DataAnnotations.Schema;
using WebTeam.Data;
using WebTeam.Data.Migrations;

namespace WebTeam.Models
{
    [Table("Article")]
    public partial class Article
    {
        public int ArticleId { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public byte[]? MagazineCover { get; set; }

        public string? AuthorID { get; set; }

        public int? CategoryId { get; set; }

        public DateTime ArticleDate { get; set; }

        public int NoOfLike { get; set; }

        [ForeignKey("AuthorID")]
        public virtual ApplicationUser? Author { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
        public int LikesCount { get; internal set; }


        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
    }
}
