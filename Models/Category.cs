using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FirstWebApplication.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Автоинкремент
        [Key]
        [Column("category_id")] 
            public int CategoryId { get; set; }

        [Required(ErrorMessage = "Название - обязательное поле!")]
        [StringLength(64, MinimumLength = 1, ErrorMessage = "Название должно быть от 1 до 64 символов!")]
        [Column("name")]
            public string Name { get; set; }

        //навигационное свойство
        [NotMapped] //Аннотация, благодарся которой
                    //EF игнорирует это свйоство при работе с БД
            public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
