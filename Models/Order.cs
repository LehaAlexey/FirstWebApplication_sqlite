using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FirstWebApplication.Models
{
    public enum OrderState
    {
        Обрабатывается,
        Доставляется,
        Доставлен,
        Отменен
    }
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Автоинкремент
        [Key]
        [Column("order_id")]
            public int OrderId { get; set; }

        [Required(ErrorMessage = "Название - обязательное поле!")]
        [Column("user_id")]
            public int UserId { get; set; }

        [NotMapped] //Это свойство не будет использоваться как атрибут БД
        public DateTime DateProperty { get; set; }

        // Это свойство будет храниться в БД как TEXT
        [Column("date")]
        [Required(ErrorMessage = "Дата - обязательное поле!")]
        public string DatePropertyText
        {
            get => DateProperty.ToString("yyyy-MM-dd"); // Преобразование DateTime в строку
            set
            {
                if (DateTime.TryParse(value, out DateTime date))
                {
                    DateProperty = date; // Преобразование строки в DateTime
                }
                else
                {
                    // Здесь можно выбросить исключение или обработать ошибку
                    throw new FormatException("Неверный формат даты!");
                }
            }
        }

        [Column("state")]
        [Required(ErrorMessage = "Статус - обязательное поле!")]
        [EnumDataType(typeof(OrderState))]
            public OrderState State { get; set; }


        //навигационное свойство
        [NotMapped] //Аннотация, благодарся которой
                    //EF игнорирует это свйоство при работе с БД
        public virtual ICollection<OrderProductConnection> OrderProductConnections { get; set; } 
            = new List<OrderProductConnection>();

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
    }
}
