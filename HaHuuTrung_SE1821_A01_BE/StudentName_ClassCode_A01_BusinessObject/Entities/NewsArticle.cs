﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject;

[Table("NewsArticle")]
public partial class NewsArticle
{
    [Key]
    [StringLength(20)]
    public string NewsArticleID { get; set; } = null!;

    [StringLength(400)]
    public string? NewsTitle { get; set; }

    [StringLength(150)]
    public string Headline { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(4000)]
    public string? NewsContent { get; set; }

    [StringLength(400)]
    public string? NewsSource { get; set; }

    public short? CategoryID { get; set; }

    public bool? NewsStatus { get; set; }

    public short? CreatedByID { get; set; }

    public short? UpdatedByID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [ForeignKey("CategoryID")]
    [InverseProperty("NewsArticles")]
    public virtual Category? Category { get; set; }

    [ForeignKey("CreatedByID")]
    [InverseProperty("NewsArticles")]
    public virtual SystemAccount? CreatedBy { get; set; }

    [ForeignKey("NewsArticleID")]
    [InverseProperty("NewsArticles")]
    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
