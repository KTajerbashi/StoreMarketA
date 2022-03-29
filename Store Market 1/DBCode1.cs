using System;
using System.Data.Entity;
using System.Linq;

namespace Store_Market_1
{
    public class DBCode1 : DbContext
    {
        #region Comment
        // Your context has been configured to use a 'DBCode1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Store_Market_1.DBCode1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DBCode1' 
        // connection string in the application configuration file.
        #endregion
        public DBCode1()
            : base("name=DBCode1")
        {

        }
        public DbSet<Admin> admins { set; get; }
        public DbSet<Customer> customers { set; get; }
        public DbSet<Agent> agents { set; get; }
        public DbSet<RoleAccess> roleAccesses { set; get; }
        public DbSet<Company> companies { set; get; }
        public DbSet<Product> products { set; get; }

        #region Comment
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        #endregion
    }
    #region Comment
    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
    #endregion
}