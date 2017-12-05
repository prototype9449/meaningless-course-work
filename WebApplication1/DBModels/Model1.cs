namespace DBModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OnlineShopContext : DbContext
    {
        public OnlineShopContext()
            : base("name=OnlineShopEntities")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Predicate> Predicates { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Groups)
                .WithMany(e => e.Employees)
                .Map(m => m.ToTable("EmployeeGroups").MapLeftKey("EmployeeId").MapRightKey("GroupId"));

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Predicates)
                .WithMany(e => e.Groups)
                .Map(m => m.ToTable("Policies").MapLeftKey("GroupId").MapRightKey("PredicateId"));

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);
        }
    }
}
