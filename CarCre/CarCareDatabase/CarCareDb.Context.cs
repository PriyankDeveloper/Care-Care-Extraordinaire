﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarCare.CarCareDatabase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CarCareEntity : DbContext
    {
        public CarCareEntity()
            : base("name=CarCareEntity")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TuneUp> TuneUps { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<ServiceRecord> ServiceRecords { get; set; }
        public virtual DbSet<ServiceStation> ServiceStations { get; set; }
        public virtual DbSet<ServiceType> ServiceTypes { get; set; }
        public virtual DbSet<RepairRecord> RepairRecords { get; set; }
        public virtual DbSet<Insurance> Insurances { get; set; }
        public virtual DbSet<Warranty> Warranties { get; set; }
    }
}
