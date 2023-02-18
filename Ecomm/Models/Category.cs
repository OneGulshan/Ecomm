using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomm.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DisplayOrder { get; set; }
        [NotMapped]
        public IFormFile CategoryImage { get; set; }
        public string CategoryImagePath { get; set; }
    }
}
