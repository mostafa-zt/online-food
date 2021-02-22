using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.ViewModels
{
    public class UserCommentViewModel
    {
        public string RestaurantPercentRate { get; set; }
        public string RestauarntRate { get; set; }
        public List<RestaurantRateGroupViewModel> RestaurantRateGroupViewModels { get; set; }
        public List<ListUserCommentViewModel> ListUserCommentViewModels { get; set; }

        public int Excellent { get; set; }
        public int Normal { get; set; }
        public int Bad { get; set; }
    }

    public class ListUserCommentViewModel
    {
        public string Firstname { get; set; }
        public DateTime? CreatorDateTime { get; set; }
        public string Text { get; set; }

        public List<SubListUserCommentViewModel> SubListUserCommentViewModels { get; set; }
    }
    public class SubListUserCommentViewModel
    {
        public string Firstname { get; set; }
        public DateTime? CreatorDateTime { get; set; }
        public string Text { get; set; }
        public string RestaurantLogo { get; set; }
    }


    public class RestaurantRateGroupViewModel
    {
        public string RestaurantRateType { get; set; }
        public string Rate { get; set; }
        public string Percent { get; set; }
    }
}
