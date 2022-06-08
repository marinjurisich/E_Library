using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Library.Models {

    public class User {

        [Key]
        public int Id { get; set; }

        public string? Username { get; set; }
        public string? Password { get; set; }  
        public string? Email { get; set; }
        public string? Role { get; set; }

        public string Guid { get; set; }

        public virtual Loan? Loan { get; set; }


        public User(int id, string username, string role) {
            Id = id;
            Username = username;
            Role = role;
        }

        public User() {
            
        }

    }
}
