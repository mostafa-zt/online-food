using OnlineFood.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Common.Utility
{
    public static class TextGenerator
    {
        public static string Fullname(string firstname, string lastname)
        {
            return string.Format("{0} {1}", firstname, lastname);
        }

        public static string FullnameWithGender(string firstname, string lastname, string gender)
        {
            return string.Format("{0}/{1} {2}", gender, firstname, lastname);
        }

        public static string Location(string city, string cityarea)
        {
            return string.IsNullOrEmpty(cityarea) ? string.Format("{0}", city) : string.Format("{0} - {1}", city, cityarea);
        }

        public static string RestaurantLink(string cityTitleEnglish, string restaurantCategoryTitleEnglish, string restaurantTitleEnglish)
        {
            return string.Format("/{0}/{1}/{2}/", cityTitleEnglish.ToLower(), restaurantCategoryTitleEnglish.ToLower(), restaurantTitleEnglish.ToLower().GetFriendlyTitle());
        }
        public static string RestaurantLinkWithWWW(string cityTitleEnglish, string restaurantCategoryTitleEnglish, string restaurantTitleEnglish)
        {
            return string.Format("www.OnlineFood.com/{0}/{1}/{2}/", cityTitleEnglish.ToLower(), restaurantCategoryTitleEnglish.ToLower(), restaurantTitleEnglish.ToLower().GetFriendlyTitle());
        }

        public static string SearchLink(string city, string cityarea = null, string search = null)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return string.IsNullOrEmpty(cityarea) ? string.Format("/restaurants/{0}/?search={1}", city.ToLower().GetFriendlyTitle(), search) : string.Format("/restaurants/{0}/{1}/?search={2}", city.ToLower().GetFriendlyTitle(), cityarea.ToLower().GetFriendlyTitle(), search);
            }
            return string.IsNullOrEmpty(cityarea) ? string.Format("/restaurants/{0}/", city.ToLower().GetFriendlyTitle()) : string.Format("/restaurants/{0}/{1}/", city.ToLower().GetFriendlyTitle(), cityarea.ToLower().GetFriendlyTitle());
        }
    }
}
