using System.ComponentModel.DataAnnotations;

namespace Client.Models.DTOs
{
    public class NewsArticleDTO
    {
        [Key]
        public string NewsArticleId { get; set; } = string.Empty;

        public string NewsTitle { get; set; } = string.Empty;
        public string? Headline { get; set; }
        public string? NewsContent { get; set; }
        public string? NewsSource { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? NewsStatus { get; set; }

        public short? CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public short? CreatedById { get; set; }
        public string? CreatedByName { get; set; }

        public short? UpdatedById { get; set; }
        public string? UpdatedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }

        // Tags
        public List<int>? TagIds { get; set; }
        public List<string>? TagNames { get; set; }
    }
}
