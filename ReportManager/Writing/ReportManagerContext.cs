using ReportManager.LogImporting;
using ReportManager.Model;
using System;
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

        public void WriteWarnings(List<WarningRecord> records)
        {
            using (var context = new ReportManagerContext())
            {
                foreach (var record in records)
                {
                    if (context.Entry(record).State == EntityState.Detached)
                    {
                        context.Database.Log = Console.WriteLine;
                        context.WarningRecords.Add(record);
                        context.Database.Log = Console.WriteLine;
                        context.SaveChanges();
                    }else if(context.Entry(record).State == EntityState.Added)
                    {
                        context.Database.Log = Console.WriteLine;
                        context.WarningRecords.Attach(record);
                        context.Database.Log = Console.WriteLine;
                        context.SaveChanges();
                    }
                }
            }
        }

        public void WriteErrors(List<ErrorRecord> records)
        {
            using (var context = new ReportManagerContext())
            {
                foreach (var record in records)
                {
                    if (context.Entry(record).State == EntityState.Detached)
                    {
                        context.Database.Log = Console.WriteLine;
                        context.ErrorRecords.Add(record);
                        context.Database.Log = Console.WriteLine;
                        context.SaveChanges();
                    }
                    else if(context.Entry(record).State == EntityState.Added)
                    {
                        context.Database.Log = Console.WriteLine;
                        context.ErrorRecords.Attach(record);
                        context.Database.Log = Console.WriteLine;
                        context.SaveChanges();
                    }
                }
            }

        }


        public void WriteSummary(Summary summary)
        {
            using (var context = new ReportManagerContext())
            {
                foreach(KeyValue tapl in summary.parts)
                {
                    if (context.Entry(tapl).State == EntityState.Detached)
                    {
                        context.Database.Log = Console.WriteLine;
                        context.KeyValues.Add(tapl);
                        context.SaveChanges();
                        context.Database.Log = Console.WriteLine;
                    }
                    else if (context.Entry(tapl).State == EntityState.Added)
                    {
                        context.KeyValues.Attach(tapl);
                        context.SaveChanges();
                    }
                }
            }
        }

        public void WriteStatistic(List<StatisticRecord> statisticRecords)
        {
            using (var context = new ReportManagerContext())
            {
                foreach (var s in statisticRecords)
                {
                    if (context.Entry(s).State == EntityState.Detached)
                    {
                        context.Database.Log = Console.WriteLine;
                        context.StatisticRecords.Add(s);
                        context.SaveChanges();
                        context.Database.Log = Console.WriteLine;
                    }
                    else if (context.Entry(s).State == EntityState.Added)
                    {
                        context.StatisticRecords.Attach(s);
                        context.SaveChanges();
                    }

                }
            }
        }
}
}