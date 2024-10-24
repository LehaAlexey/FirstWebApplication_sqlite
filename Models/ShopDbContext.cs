using Microsoft.EntityFrameworkCore;

namespace FirstWebApplication.Models
{
    public class ShopDbContext: DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options): base(options)
        {
        }
        //Определим DbSet-ы для каждой сущности.
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderProductConnection> orderProductConnections { get; set; }

        //переопределяем метод OnConfiguring для реализации подключения к БД
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlite("Data Source=shop_db.db"); //Указываем путь к БД
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.UserId);          //Добавляем связь Один-ко-многим
                                                        //между User и Order


            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);          //Добавляем связь Один-ко-многим
                                                        //между Category и Product

            modelBuilder.Entity<OrderProductConnection>()
                .HasKey(op => new { op.OrderId, op.ProductId }); //добавляем составной ключ
                                                                 //с помощью .HasKey()

            modelBuilder.Entity<OrderProductConnection>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProductConnections)
                .HasForeignKey(op => op.OrderId);               //Добавляем связь Один-ко-многим
                                                                //для свойства-части ПК OrderId

            modelBuilder.Entity<OrderProductConnection>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrderProductConnections)
                .HasForeignKey(op => op.ProductId);             //Добавляем связь Один-ко-многим
                                                                //для свойства-части ПК ProductId
        }
    }
}
