using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TrainingModels;
namespace TrainingDataAccess
{
    public class TrainingEntityContext : DbContext
    {
        public TrainingEntityContext()
            : base("name=DbConnectionString")
        {
        }
        public DbSet<Training> Training { get; set; }
    }
}
