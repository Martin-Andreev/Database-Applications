namespace _02.ToList
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using _01.ShowDataFromRelatedTables;

    public class ToListMain
    {
        public static void Main()
        {
            var context = new AdsEntities();

            TimeSpan[] optimizedTestDurations = new TimeSpan[10];
            TimeSpan[] nonOptimizedTestDurations = new TimeSpan[10];

            Console.WriteLine("Non- Optimized:");
            for (int i = 0; i < 10; i++)
            {
                nonOptimizedTestDurations[i] = ListAdsNonOptimizedVersion(context);
                Console.WriteLine("Run {0}: {1}", i + 1, nonOptimizedTestDurations[i]);
            }

            Console.WriteLine("\nOptimized:");
            for (int i = 0; i < 10; i++)
            {
                optimizedTestDurations[i] = ListAdsOptimizedVersion(context);
                Console.WriteLine("Run {0}: {1}", i + 1, optimizedTestDurations[i]);
            }

            Console.WriteLine("\nNon-optimized average: {0} Ticks{1}Optimized average: {2} Ticks",
                nonOptimizedTestDurations.Average(n => n.Ticks),
                Environment.NewLine,
                optimizedTestDurations.Average(o => o.Ticks));
        }

        private static TimeSpan ListAdsOptimizedVersion(AdsEntities context)
        {
            context.Database.SqlQuery<Ad>("CHECKPOINT; DBCC DROPCLEANBUFFERS;");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var ad = context.Ads
                .Where(a => a.AdStatus.Status.Equals("Published"))
                .OrderBy(a => a.Date)
                .Select(a => new
                {
                    AdTitle = a.Title,
                    AdCategory = a.Category == null ? string.Empty : a.Category.Name,
                    AdTown = a.Town == null ? string.Empty : a.Town.Name,
                    AdPublishedOn = a.Date
                })
                .ToList();

            var timeSpent = stopwatch.Elapsed;
            stopwatch.Reset();

            return timeSpent;
        }

        private static TimeSpan ListAdsNonOptimizedVersion(AdsEntities context)
        {
            context.Database.SqlQuery<Ad>("CHECKPOINT; DBCC DROPCLEANBUFFERS;");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var ad = context.Ads
                .ToList()
                .Where(a => a.AdStatus.Status.Equals("Published"))
                .Select(a => new
                {
                    AdTitle = a.Title,
                    AdCategory = a.Category == null ? string.Empty : a.Category.Name,
                    AdTown = a.Town == null ? string.Empty : a.Town.Name,
                    AdPublishedOn = a.Date
                })
                .ToList()
                .OrderBy(a => a.AdPublishedOn);

            var timeSpent = stopwatch.Elapsed;
            stopwatch.Reset();

            return timeSpent;
        }
    }
}
