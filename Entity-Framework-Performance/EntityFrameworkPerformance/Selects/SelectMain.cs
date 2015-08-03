namespace Selects
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using _01.ShowDataFromRelatedTables;

    public class SelectMain
    {
        public static void Main()
        {
            var context = new AdsEntities();

            TimeSpan[] nonOptimizedTestDurations = new TimeSpan[10];
            TimeSpan[] optimizedTestDurations = new TimeSpan[10];

            Console.WriteLine("Non-optimized:");
            for (int i = 0; i < 10; i++)
            {
                nonOptimizedTestDurations[i] = GetTimeForNonOptimizedSelect(context);
                Console.WriteLine("Run {0}: {1}", i + 1, nonOptimizedTestDurations[i]);
            }

            Console.WriteLine("\nOptimized:");
            for (int i = 0; i < 10; i++)
            {
                optimizedTestDurations[i] = GetTimeForOptimizedSelect(context);
                Console.WriteLine("Run {0}: {1}", i + 1, optimizedTestDurations[i]);
            }

            Console.WriteLine("\nNon-optimized avarage: {0} Ticks{1}Optimized avarage: {2} Ticks",
                nonOptimizedTestDurations.Average(n => n.Ticks),
                    Environment.NewLine,
                    optimizedTestDurations.Average(n => n.Ticks));
        }

        private static TimeSpan GetTimeForOptimizedSelect(AdsEntities context)
        {
            context.Database.SqlQuery<Ad>("CHECKPOINT; DBCC DROPCLEANBUFFERS;");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var ads = context.Ads.Select(a => a.Title);
            string tempAdName;

            foreach (var ad in ads)
            {
                tempAdName = ad;
            }

            var timeSpent = stopwatch.Elapsed;
            stopwatch.Reset();

            return timeSpent;
        }

        private static TimeSpan GetTimeForNonOptimizedSelect(AdsEntities context)
        {
            context.Database.SqlQuery<Ad>("CHECKPOINT; DBCC DROPCLEANBUFFERS;");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var ads = context.Ads;
            string tempAdName;

            foreach (var ad in ads)
            {
                tempAdName = ad.Title;
            }

            var timeSpent = stopwatch.Elapsed;
            stopwatch.Reset();

            return timeSpent;
        }
    }
}
