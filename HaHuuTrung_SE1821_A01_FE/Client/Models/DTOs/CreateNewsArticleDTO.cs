namespace Client.Models.DTOs
{
    public class CreateNewsArticleDTO
    {
        public string NewsTitle { get; set; } = null!;
        public string Headline { get; set; } = null!;
        public string? NewsContent { get; set; }
        public string? NewsSource { get; set; }
        public bool NewsStatus { get; set; }
        public short CategoryId { get; set; }

        public short CreatedById { get; set; } 
        public short? UpdatedById { get; set; }

        public List<int> TagIds { get; set; } = new();
    }
}
