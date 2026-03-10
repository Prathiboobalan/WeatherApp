using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace BlazorApp.Models
{
    [Table("users")]
    public class User : BaseModel
    {
        

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [PrimaryKey("email", false)]

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("cities")]
        public List<string> Cities { get; set; } = new();

        [Column("role")]
        public string Role { get; set; } = string.Empty;
    }
}

