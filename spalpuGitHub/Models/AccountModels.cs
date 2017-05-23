using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace spalpuGitHub.Models
{
    public class AccountModels
    {
    }
    public class UserDbContext : DbContext
    {
        public UserDbContext()
            : base("name = UserConnectionString")
        {
        }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
    }

    public class UserAccount
    {
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool ConfirmEmail { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }

    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public UserAccount Owner { get; set; }
    }

    public class ResisterLoginModel
    {
        [Required(ErrorMessage = "Требуется указать отображаемое имя")]
        [Display(Name = "Отображаемое имя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Требуется указать email")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3]\.)|(([\w-]+\.)+))([a-zA-Z{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Введите действительный адрес электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Требуется указать пароль")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Требуется подтвердить пароль")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}