using ReportManager.LogImporting;
using ReportManager.Model;
using System.Collections.Generic;
using System.Data.Entity;

namespace ReportManager.Writing
{
    class ReportManagerContext : DbContext
    {
        public DbSet<StatisticRecord> StatisticRecords { set; get; }
        public DbSet<KeyValue> KeyValues { get; set; }
        public DbSet<WarningRecord> WarningRecords { set; get; }
        public DbSet<ErrorRecord> ErrorRecords { set; get; }

        public void Write<T>(List<T> records, DbSet<T> dbset) where T : class
        {
            foreach (var record in records)
            {
                if (Entry(record).State == EntityState.Detached)
                {
                    dbset.Add(record);
                    SaveChanges();
                }
                else if (Entry(record).State == EntityState.Added)
                {
                    dbset.Attach(record);
                    SaveChanges();
                }
            }
        }

        public void WriteSummary(Summary summary)
        {
            foreach (KeyValue tapl in summary.parts)
            {
                if (Entry(tapl).State == EntityState.Detached)
                {
                    KeyValues.Add(tapl);
                    SaveChanges();
                }
                else if (Entry(tapl).State == EntityState.Added)
                {
                    KeyValues.Attach(tapl);
                    SaveChanges();
                }
            }
        }
    }
}