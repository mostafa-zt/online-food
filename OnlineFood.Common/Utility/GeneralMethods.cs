using OnlineFood.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Common.Utility
{
    public static class GeneralMethods
    {
        public static Color ChooseColor(Domain.Enum.ActivityStatus activityStatus)
        {
            Color result;
            switch (activityStatus)
            {
                case Domain.Enum.ActivityStatus.Available:
                    result = Color.Green;
                    break;
                case Domain.Enum.ActivityStatus.UnAvailable:
                    result = Color.Red;
                    break;
                default:
                    result = Color.Red;
                    break;
            }
            return result;
        }


        public static Color ChooseColor(Domain.Enum.SellerRegistrationProcess sellerRegistrationProcess)
        {
            Color result;
            switch (sellerRegistrationProcess)
            {
                case Domain.Enum.SellerRegistrationProcess.SubmitApplication:
                    result = Color.Red;
                    break;
                case Domain.Enum.SellerRegistrationProcess.DocumentConfirmation:
                    result = Color.Cyan;
                    break;
                case Domain.Enum.SellerRegistrationProcess.RestaurantRegistration:
                    result = Color.Green;
                    break;
                //case Domain.Enum.SellerRegistrationProcess.MenuRegistration:
                //    result = Color.Blue;
                //    break;
                //case Domain.Enum.SellerRegistrationProcess.ReadyToOrder:
                //    result = Color.Green;
                //    break;
                default:
                    result = Color.Red;
                    break;
            }
            return result;
        }


        public static Color ChooseColor(Domain.Enum.RestaurantActivityStatus restaurantActivityStatus)
        {
            Color result;
            switch (restaurantActivityStatus)
            {
                case Domain.Enum.RestaurantActivityStatus.ReadyToOrder:
                    result = Color.Green;
                    break;
                case Domain.Enum.RestaurantActivityStatus.UnReadyToOrder:
                    result = Color.Red;
                    break;
                default:
                    result = Color.Red;
                    break;
            }
            return result;
        }
    }
}
