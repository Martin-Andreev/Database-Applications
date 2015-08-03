namespace _01.ShowDataFromRelatedTables
{
    using System;
    using System.Data.Entity;

    public class AdsMain
    {
        public static void Main()
        {
            var context = new AdsEntities();

            ListAllAdsWithoutInclude(context);

            //ListAllAdsWithInclude(contex);
        }

        private static void ListAllAdsWithoutInclude(AdsEntities contex)
        {
            foreach (var ad in contex.Ads)
            {
                Console.WriteLine("Ad Name: {0}{5}Status: {1}{5}Category: {2}{5}Town: {3}{5}User: {4}{5}",
                    ad.Title,
                    ad.AdStatus.Status,
                    ad.Category == null ? string.Empty : ad.Category.Name,
                    ad.Town == null ? string.Empty : ad.Town.Name,
                    ad.AspNetUser.UserName,
                    Environment.NewLine);
            }
        }

        private static void ListAllAdsWithInclude(AdsEntities contex)
        {
            var ads = contex.Ads
                .Include(a => a.Category)
                .Include(a => a.Town)
                .Include(a => a.AdStatus)
                .Include(a => a.AspNetUser);

            foreach (var ad in ads)
            {
                Console.WriteLine("Ad Name: {0}{5}Status: {1}{5}Category: {2}{5}Town: {3}{5}User: {4}{5}",
                    ad.Title,
                    ad.AdStatus.Status,
                    ad.Category == null ? string.Empty : ad.Category.Name,
                    ad.Town == null ? string.Empty : ad.Town.Name,
                    ad.AspNetUser.UserName,
                    Environment.NewLine);
            }

            //Second way
            //var ads = contex.Ads
            //    .Select(a => new
            //    {
            //        Title = a.Title,
            //        Status = a.AdStatus.Status,
            //        Category = a.Category.Name,
            //        Town = a.Town.Name,
            //        User = a.AspNetUser.UserName
            //    });

            //foreach (var ad in ads)
            //{
            //    Console.WriteLine("Ad Name: {0}{5}Status: {1}{5}Category: {2}{5}Town: {3}{5}User: {4}{5}",
            //        ad.Title,
            //        ad.Status,
            //        ad.Category,
            //        ad.Town,
            //        ad.User,
            //        Environment.NewLine);
            //}
        }
    }
}
