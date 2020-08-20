using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace WpfApp_D_NrimanProject.Model
{
    class AppDB : DbContext
    {
        public AppDB() { }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Glass> Glasses { get; set; }
        public DbSet<Cleaner> Cleaners { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\ملفات من الهارد القديم\repos\WpfApp_D_NrimanProject\Database1.mdf;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasKey(i => i.ItemID);
            modelBuilder.Entity<Bill>().HasKey(b => b.BillID);
            modelBuilder.Entity<Employee>().HasKey(b => b.EmployeeID);

            modelBuilder.Entity<Item>().HasOne(f => f.Bill).WithMany(b => b.Items);

            var converter = new ValueConverter<ItemType, string>(v => v.ToString(), v => (ItemType)Enum.Parse(typeof(ItemType), v));
            modelBuilder.Entity<Item>().Property(e => e.ItemType).HasConversion(converter);
        }
    }

    #region DataBase Entitys

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Admin { get; set; }
    }

    public class Item
    {
        public int ItemID { get; }
        public DateTime InTime { set; get; }
        public DateTime? OutTime { set; get; }
        public double Price { set; get; }
        public ItemType ItemType { protected set; get; }
        public string Name { get; set; }
        public bool Sold { get; set; }

        [ForeignKey("Bill")]
        public int? BillID { get; set; }
        public Bill Bill { get; set; }
    }

    public class Food : Item
    {
        public Food()
        {
            ItemType = ItemType.Food;
        }

        public DateTime EndTime { set; get; }
        public string FoodType { set; get; }
    }

    public class Glass : Item
    {
        public Glass()
        {
            ItemType = ItemType.Glass;
        }

        public double Width { get; set; }
        public double Capacity { get; set; }
    }

    public class Cleaner : Item
    {
        public Cleaner()
        {
            ItemType = ItemType.Cleaner;
        }
        public string Company { get; set; }
    }

    public class Bill
    {
        public Bill()
        {
            Items = new List<Item>();
        }

        [Key]
        public int BillID { get; }
        public string CustomerName { get; set; }
        public DateTime OutTime { get; set; }

        public List<Item> Items { get; set; }
    }

    #endregion

    public enum ItemType
    {
        Food,
        Glass,
        Cleaner
    }

    //public class ViewModel
    //{
    //    public ViewModel() { }
    //    public ViewModel(Food food)
    //    {
    //        ID = food.ItemID;
    //        Name = food.Name;
    //        ItemType = food.ItemType;
    //        Price = food.Price;
    //        InTime = food.InTime;
    //        FoodType = food.FoodType;
    //        ManufactureCompany = "-";
    //        Width = 0;
    //        Capacity = 0;
    //    }
    //    public ViewModel(Glass glass)
    //    {
    //        ID = glass.ItemID;
    //        Name = glass.Name;
    //        ItemType = glass.ItemType;
    //        Price = glass.Price;
    //        InTime = glass.InTime;
    //        FoodType = "-";
    //        ManufactureCompany = "-";
    //        Width = glass.Width;
    //        Capacity = glass.Capacity;
    //    }
    //    public ViewModel(Cleaner cleaner)
    //    {
    //        ID = cleaner.ItemID;
    //        Name = cleaner.Name;
    //        ItemType = cleaner.ItemType;
    //        Price = cleaner.Price;
    //        InTime = cleaner.InTime;
    //        FoodType = "-";
    //        ManufactureCompany = cleaner.Company;
    //        Width = 0;
    //        Capacity = 0;
    //    }
    //    public int ID { get; set; }
    //    public string Name { get; set; }
    //    public ItemType ItemType { get; set; }
    //    public double Price { get; set; }
    //    public DateTime InTime { set; get; }
    //    public string FoodType { get; set; }
    //    public string ManufactureCompany { get; set; }
    //    public double Width { get; set; }
    //    public double Capacity { get; set; }
    //}
    public class BillsViewModel
    {
        public BillsViewModel()
        {
            OutTime = new DateTime();
        }
        public int BillID { get; set; }
        public double TotalPrice { get; set; }
        public int ItemsCount { get; set; }
        public string CustomerName { get; set; }
        public DateTime OutTime { get; set; }
    }
}
