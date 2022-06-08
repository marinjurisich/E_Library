using HotChocolate.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Library.Models {
    
    public class Loan {
        [Key]
        public int Id { get; set; }
        
        public int User_id { get; set; }

        [Authorize]
        [ForeignKey("User_id")]
        public virtual User User { get; set; }

        public int Book_id { get; set; }
        [ForeignKey("Book_id")]
        public virtual Book Book { get; set; }

    }
    
}
