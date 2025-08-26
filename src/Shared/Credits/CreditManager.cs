using System;

namespace CampusLove_hadassa_dylan.src.Shared.Credits
{
    public static class CreditManager
    {
        private static int maxLikesPerDay = 10;
        private static int likesToday = 0;
        private static DateTime lastLikeDate = DateTime.Today;

        public static bool CanLike()
        {
            if (DateTime.Today != lastLikeDate)
            {
                likesToday = 0;
                lastLikeDate = DateTime.Today;
            }
            return likesToday < maxLikesPerDay;
        }

        public static void UseLike()
        {
            if (DateTime.Today != lastLikeDate)
            {
                likesToday = 0;
                lastLikeDate = DateTime.Today;
            }
            likesToday = Math.Min(likesToday + 1, maxLikesPerDay);
        }

        public static int LikesRestantes()
        {
            if (DateTime.Today != lastLikeDate)
            {
                likesToday = 0;
                lastLikeDate = DateTime.Today;
            }
            return Math.Max(0, maxLikesPerDay - likesToday);
        }
    }
}
