﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DormitoryApp.Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DatabaseModelContext : DbContext
    {
        public DatabaseModelContext()
            : base("name=DatabaseModelContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<UserModel> UserModels { get; set; }
        public virtual DbSet<DormitoryModel> DormitoryModels { get; set; }
        public virtual DbSet<EmployeeModel> EmployeeModels { get; set; }
        public virtual DbSet<EmployeeLogModel> EmployeeLogModels { get; set; }
        public virtual DbSet<ResidentModel> ResidentModels { get; set; }
        public virtual DbSet<RoomModel> RoomModels { get; set; }
        public virtual DbSet<MenuButtonModel> MenuButtonModels { get; set; }
    }
}
