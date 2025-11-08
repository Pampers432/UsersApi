using System.ComponentModel.DataAnnotations;

namespace UsersApi.Models
{
    public class UserRole
    {
        public int Id { get; set; }

        [Required]
        public string Role { get; set; }

        public UserRole() { }

        private UserRole(string role)
        {
            Role = role;
        }

        //public List<UserRole> CreateRoles()
        //{
        //    return new List<UserRole>() { new UserRole(), new UserRole("Admin") };
        //}
    }
}
