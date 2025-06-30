
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poultry_website.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required] public string FullName { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
        [Required] public string PasswordHash { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }

}
