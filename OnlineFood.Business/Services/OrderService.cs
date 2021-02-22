using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineFood.Business.Contracts;
using OnlineFood.Common.Model;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineFood.Business.Services
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private ApplicationDbContext _dbContext;
        private DbSet<Order> query;

        private const string OnlineFoodOrderCart = "OnlineFoodcart";

        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Ctor
        public OrderService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext,
                            IHttpContextAccessor httpContextAccessor) : base(unitOfWork, dbContext)
        {
            _unitOfWork = unitOfWork;
            query = _unitOfWork.Set<Order>();
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion


        /// <summary>  
        /// Get the cookie  
        /// </summary>  
        /// <param name="key">cookie key</param>  
        /// <returns>string value</returns>  
        public string GetCookie(string key)
        {
            return _httpContextAccessor.HttpContext.Request.Cookies[key];
        }

        /// <summary>
        /// set cookie to http context response
        /// </summary>
        /// <param name="key">cookie key</param>
        /// <param name="value">cookie value</param>
        /// <param name="expireTime">cookie expire time as day</param>
        public void SetCookie(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddDays(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
        }

        /// <summary>  
        /// remove cookie
        /// </summary>  
        /// <param name="key">cookie key</param>  
        public void RemoveCookie(string key = OnlineFoodOrderCart)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
        }

        /// <summary>  
        /// is exist cookie
        /// </summary>  
        /// <param name="key">cookie key</param>  
        public bool IsExistCookie(string key = OnlineFoodOrderCart)
        {
            return _httpContextAccessor.HttpContext.Request.Cookies[key] != null;
        }

        public List<OrderBasket> InsertToCookie(int menuId, decimal price, string title)
        {
            List<OrderBasket> order = null;
            if (IsExistCookie(OnlineFoodOrderCart))
            {
                order = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderBasket>>(GetCookie(OnlineFoodOrderCart));
                var alreadyExist = order.FirstOrDefault(x => x.MenuId == menuId);
                if (alreadyExist != null)
                {
                    ++alreadyExist.Number;
                }
                else
                {
                    order.Add(new OrderBasket()
                    {
                        Number = 1,
                        MenuId = menuId,
                        Price = price,
                        Title = title
                    });
                }
            }
            else
            {
                order = new List<OrderBasket>()
                {
                    new  OrderBasket()
                    {
                         Number = 1,
                         MenuId = menuId,
                         Price =price ,
                         Title = title
                    }
                };
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(order);
            SetCookie(OnlineFoodOrderCart, json, 7);
            return order;
        }

        public List<OrderBasket> IncreaseToCookie(int menuId)
        {
            List<OrderBasket> order = null;
            if (IsExistCookie(OnlineFoodOrderCart))
            {
                order = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderBasket>>(GetCookie(OnlineFoodOrderCart));
                var alreadyExist = order.FirstOrDefault(x => x.MenuId == menuId);
                if (alreadyExist != null)
                {
                    ++alreadyExist.Number;
                }
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(order);
            SetCookie(OnlineFoodOrderCart, json, 7);
            return order;
        }

        public List<OrderBasket> DecreaseToCookie(int menuId)
        {
            List<OrderBasket> order = null;
            if (IsExistCookie(OnlineFoodOrderCart))
            {
                order = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderBasket>>(GetCookie(OnlineFoodOrderCart));
                var alreadyExist = order.FirstOrDefault(x => x.MenuId == menuId);
                if (alreadyExist != null && alreadyExist.Number > 1)
                {
                    --alreadyExist.Number;
                }
                else if (alreadyExist.Number == 1)
                {
                    order = RemoveItemInCookie(menuId);
                }
            }
            if (order == null || !order.Any())
            {
                RemoveCookie(OnlineFoodOrderCart);
                return new List<OrderBasket>();
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(order);
            SetCookie(OnlineFoodOrderCart, json, 7);
            return order;
        }

        public List<OrderBasket> RemoveItemInCookie(int menuId)
        {
            List<OrderBasket> order = null;
            if (IsExistCookie(OnlineFoodOrderCart))
            {
                order = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderBasket>>(GetCookie(OnlineFoodOrderCart));
                var found = order.FirstOrDefault(x => x.MenuId == menuId);
                if (found != null)
                {
                    order.Remove(found);
                }
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(order);
            SetCookie(OnlineFoodOrderCart, json, 7);
            return order;
        }

        public int TotalNumberOfOrderCart(List<OrderBasket> orders)
        {
            int totalnumber = 0;
            foreach (var item in orders)
            {
                totalnumber += item.Number;
            }
            return totalnumber;
        }

        public decimal TotalPriceOfOrderCart(List<OrderBasket> orders)
        {
            decimal totalprice = 0;
            foreach (var item in orders)
            {
                totalprice = totalprice + (item.Price * item.Number);
            }
            return totalprice;
        }

        public bool HasItemInCookie(int menuId)
        {
            return IsExistCookie() ? Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderBasket>>(GetCookie(OnlineFoodOrderCart)).Any(a => a.MenuId == menuId) : false;
        }

        public List<OrderBasket> GetItemsOfCookie()
        {
            return IsExistCookie() ? Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderBasket>>(GetCookie(OnlineFoodOrderCart)) : new List<OrderBasket>();
        }

        public OrderBasket GetItemOfCookie(int menuId)
        {
            return IsExistCookie() ? Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderBasket>>(GetCookie(OnlineFoodOrderCart)).FirstOrDefault(x => x.MenuId == menuId) : new OrderBasket();
        }

    }
}
