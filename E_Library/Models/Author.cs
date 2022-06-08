using System.ComponentModel.DataAnnotations;

namespace E_Library.Models {
    public class Author {

        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string? DateOfBirth { get; set; }

        public List<Book> Books { get; set; }

    }
}
