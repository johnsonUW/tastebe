﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TasteContext : DbContext
    {
        public TasteContext()
            : base("name=TasteContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cuisine> Cuisine { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<OrderedDish> OrderedDishes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<Deposit> Deposits { get; set; }
        public virtual DbSet<Preference> Preferences { get; set; }
    }
}
