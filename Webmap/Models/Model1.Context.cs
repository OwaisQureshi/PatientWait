﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Webmap.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBPATIENTNOWAITDataEntities : DbContext
    {
        public DBPATIENTNOWAITDataEntities()
            : base("name=DBPATIENTNOWAITDataEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<CLINIC> CLINICS { get; set; }
        public virtual DbSet<USER> USERs { get; set; }
        public virtual DbSet<DOCTORSUSERRATING> DOCTORSUSERRATINGS { get; set; }
        public virtual DbSet<DOCTOR> DOCTORS { get; set; }
    }
}
