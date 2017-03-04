using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.IO;

namespace DataStore
{
    public class CustomerDbInitializer : DropCreateDatabaseAlways<CustomerDbContext>
    {
        protected override void Seed(CustomerDbContext context)
        {
            string path = "..//..//..//DataStore//ScriptSQL//";
            if (File.Exists(path+"FillOrdersTable.sql") || File.Exists(path + "FillOrdersTable.sql"))
            {        
                context.Database.ExecuteSqlCommand(File.ReadAllText(path+"FillCustomersTable.sql"));
                context.Database.ExecuteSqlCommand(File.ReadAllText(path+"FillOrdersTable.sql"));
            }

        }
    }
}