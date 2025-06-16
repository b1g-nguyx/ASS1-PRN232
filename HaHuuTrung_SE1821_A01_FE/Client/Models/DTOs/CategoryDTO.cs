namespace Client.Models.DTOs
{
    public class CategoryDTO
    {
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string CategoryDesciption { get; set; }
        public bool IsActive { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
