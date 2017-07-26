using ReportManager.LogImporting;
using ReportManager.Model;
using ReportManager.Model.Report;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Windows;

namespace ReportManager.Writing
{
    class ReportManagerContext : DbContext
    {
        public DbSet<StatisticRecord> StatisticRecords { set; get; }
        public DbSet<KeyValue> KeyValues { get; set; }
        //public DbSet<Record> WarningRecords { set; get; }

        
        public void WriteErrorWarnings(IEnumerable<Record> records)
        {
            using (var context = new ReportManagerContext())
            {
                foreach (var record in records)
                {
                    if (context.Entry(record).State == EntityState.Detached)
                    {
                        context.Database.Log = Console.WriteLine;
                        //context.WarningRecords.Add(record);
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
                    context.Database.Log = Console.WriteLine;
                    context.KeyValues.Add(tapl);
                    context.Database.Log = Console.WriteLine;
                    context.SaveChanges();
                }
               
            }
        }

        public void WriteStatistic(List<StatisticRecord> statisticRecords)
        {
            //using (var context = new ReportManagerContext())
            //{
                /*foreach (var s in statisticRecords)
                {
                    if (context.Entry(s).State == EntityState.Detached)
                    {
                        context.Database.Log=Console.WriteLine;
                        context.StatisticRecords.Add(s);
                       context.Entry(s).State = EntityState.Modified;
                        context.SaveChanges();
                        Console.WriteLine();
                        MessageBox.Show(context.Entry(s).State.ToString());
                    }
                    else if(context.Entry(s).State == EntityState.Added)
                    {
                        context.StatisticRecords.Attach(s);
                        context.SaveChanges();
                        MessageBox.Show(context.Entry(s).State.ToString());
                    }
                    MessageBox.Show(context.Entry(s).State.ToString());
                    //context.StatisticRecords.Add(s);
                   
                }
                /*context.StatisticRecords.AddRange(statisticRecords);
                context.Database.Log = Console.WriteLine;
                context.SaveChanges();
                context.Database.Log = Console.WriteLine;*/
            //}
        //}
    }
}
}