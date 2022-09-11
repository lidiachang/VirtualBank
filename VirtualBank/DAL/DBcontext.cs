using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using VirtualBank.Models;

namespace VirtualBank.DAL
{
    public class DBcontext : IdentityDbContext
    {
        public DBcontext()
      : base("DefaultConnection")
        {
        }
        public DBcontext(string connectionName)
        : base(connectionName)
        {
        }
        public static DBcontext Create()
        {
            return new DBcontext();
        }
        #region Tables
        //--dbo

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Transaction> Transaction { get; set; }

        #endregion

    }
}