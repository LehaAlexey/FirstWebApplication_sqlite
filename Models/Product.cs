using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebApplication.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Автоинкремент
        [Column("product_id")]
        [Key]
            public int ProductId { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "Название - обязательное поле!")]
        [StringLength(64, MinimumLength = 1, ErrorMessage = "Название должно быть от 1 до 64 символов!")]
            public string? Name { get; set; }

        [Column("price")]
        [Required(ErrorMessage = "Цена - обязательное поле!")]
        [Range(0,1000000000, ErrorMessage = "Цена должна быть от 0 до 1.000.000.000!")]
            public decimal Price { get; set; }

        [Column("category_id")]
        [Required(ErrorMessage = "Категория - обязательное поле!")] //Добавлять Аннотации-ограничения ещё раз не нужно, т.к. данное свойство - это внешний ключ. 
            public int CategoryId { get; set; }

        //навигационные свойства
        [NotMapped] //Аннотация, благодарся которой
                    //EF игнорирует это свйоство при работе с БД
        public virtual ICollection<OrderProductConnection> OrderProductConnections { get; set; } 
            = new List<OrderProductConnection>();

        [ForeignKey("CategoryId")] //определяем CategoryId как внешний ключ к сущности Category 
                                   //virtual - указывает, что свойство "навигационное"
            public virtual Category? Category { get; set; }
    }
}
