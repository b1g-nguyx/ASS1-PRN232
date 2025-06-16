using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class CategoryDTO
    {
        public int? CategoryId { get; set; }
        public string Name { get; set; }

        public string CategoryDesciption { get; set; }

        public bool? IsActive { get; set; }
        public short? ParentCategoryId { get; set; }
    }
}
