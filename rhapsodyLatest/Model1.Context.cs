﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace rhapsodyLatest
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using rhapsodyLatest.Auth;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class rhapsody_apkEntities : DbContext
    {
        public rhapsody_apkEntities()
            : base("name=rhapsody_apkEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<apk> apks { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
    }
}
