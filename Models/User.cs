using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebApplication.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Автоинкремент
        [Column("user_id")]
        [Key] 
            public int UserId { get; set; }

        [Column("last_name")]
        [Required(ErrorMessage = "Фамилия - обязательное поле!")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Длина фамилии должна быть от 2 до 32 символов!")] 
            public string LastName { get; set; }

        [Column("first_name")]
        [Required(ErrorMessage = "Имя - бязательное поле!")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 32 символов!")] 
            public string FirstName { get; set; }

        [Column("middle_name")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Длина отчества должна быть от 2 до 32 символов!")] 
            public string? MiddleName { get; set; }

        [Column("age")]
        [Required(ErrorMessage = "Возраст - обязательное поле!")]
        [Range(4,150)]
            public int Age { get; set; }


        //Навигационное свойство
        [NotMapped] //Аннотация, благодарся которой
                    //EF игнорирует это свйоство при работе с БД
            public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
