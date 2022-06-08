using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Library.Models {

    public class Book {

        [Key]
        public int Id { get; set; }

        public string? Isbn { get; set; }
        public string? Year { get; set; }  
        public string? Title { get; set; }
        public string? Price { get; set; }
        public string? Edition_statement { get; set; }
        public string? Item_call_number { get; set; }
        public string? Publisher_code { get; set; }
        public string? Date_created { get; set; }
        public string? Number_of_copies { get; set; }

        [ForeignKey("Author")]
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
        public virtual Loan? Loan { get; set; }

    }
}
