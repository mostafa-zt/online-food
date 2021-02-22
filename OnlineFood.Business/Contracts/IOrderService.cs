using OnlineFood.Common.Model;
using OnlineFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Business.Contracts
{
    public interface IOrderService : IBaseService<Order>
    {
        /// <summary>  
        /// Get the cookie  
        /// </summary>  
        /// <param name="key">cookie key</param>  
        /// <returns>string value</returns>  
        string GetCookie(string key);

        /// <summary>
        /// set cookie to http context response
        /// </summary>
        /// <param name="key">cookie key</param>
        /// <param name="value">cookie value</param>
        /// <param name="expireTime">cookie expire time as day</param>
        void SetCookie(string key, string value, int? expireTime);

        /// <summary>  
        /// remove cookie
        /// </summary>  
        /// <param name="key">cookie key</param>  
        void RemoveCookie(string key);

        /// <summary>  
        /// is exist cookie
        /// </summary>  
        /// <param name="key">cookie key</param>  
        bool IsExistCookie(string key);

        /// <summary>
        /// insert to cookie
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="price"></param>
        /// <param name="title"></param>
        /// <returns>list of orderbasket model</returns>
        List<OrderBasket> InsertToCookie(int menuId, decimal price, string title);

        /// <summary>
        /// increase number
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        List<OrderBasket> IncreaseToCookie(int menuId);

        /// <summary>
        /// decrease number
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        List<OrderBasket> DecreaseToCookie(int menuId);

        /// <summary>
        /// remove item
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        List<OrderBasket> RemoveItemInCookie(int menuId);

        /// <summary>
        /// total number 
        /// </summary>
        /// <returns>int</returns>
        int TotalNumberOfOrderCart(List<OrderBasket> orders);

        /// <summary>
        /// total price
        /// </summary>
        /// <returns>decimal</returns>
        decimal TotalPriceOfOrderCart(List<OrderBasket> orders);

        /// <summary>
        /// is exist item in cookie
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        bool HasItemInCookie(int menuId);

        /// <summary>
        /// get all items of cookie
        /// </summary>
        /// <returns></returns>
        List<OrderBasket> GetItemsOfCookie();

        /// <summary>
        /// get specific item of cookie
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        OrderBasket GetItemOfCookie(int menuId);
    }
}
