using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FirstWebApplication.Models
{
    public class OrderProductConnection
    {
        //Логика составного ПК прописывается в реализации DbContext.
        //Здесь достаточно оставить лишь свойства и приведение столбцов
        [Column("order_id")]
            public int OrderId { get; set; }

        [Column("product_id")]
            public int ProductId { get; set; }

        [Column("quantity")]
        [Required(ErrorMessage = "Количество - обязательное поле!")]
        [Range(1, 1000, ErrorMessage = "Количество может быть от 1 до 1.000!")]
            public int Quantity { get; set; }

        // Навигационные свойства
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
