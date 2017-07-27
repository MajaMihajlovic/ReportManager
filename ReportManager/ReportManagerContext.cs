using ReportManager.Model;
using ReportManager.Model.Report;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportManager
{
    public class ReportManagerContext : DbContext
    {
        public DbSet<Record> StatisticRecords { get; set; }
        public DbSet<WarningErrorRecord> ErrorRecords { set; get; }
        public DbSet<Dictionary<string, int>> Summary {set;get;}
    }
}
