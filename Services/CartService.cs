using DbConnection;
using Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbConnection.Models;

namespace Services
{
    public class CartService : ICartService
    {
        public static DataContext _dataContext;
        public readonly ICacheService _cacheService;
        public readonly IQueueService _queueService;
        
        public CartService(DataContext dataContext, ICacheService cacheService, IQueueService queueService)
        {
            _queueService = queueService;
            _cacheService = cacheService;
            _dataContext = dataContext;
        }

        public bool AddToCart(Guid ProductId)
        {
            try
            {
                string userIdStr = SessionService.Instance.Account.AccountId.ToString();

                Guid userId; Guid.TryParse(userIdStr, out userId);

                if (!_dataContext.Carts.Any(x => x.Account.AccountId == userId && x.CartStatus == CartStatuses.Active))
                {
                    _dataContext.Carts.Attach(new Carts
                    {
                        Account = SessionService.Instance.Account,
                        CartStatus = CartStatuses.Active,
                    });

                    _dataContext.SaveChanges();
                }

                var cart = _dataContext.Carts.First(x => x.Account.AccountId == userId && x.CartStatus == CartStatuses.Active);
                var product = _dataContext.Products.First(x => x.ProductId == ProductId && x.ProductStatus == ProductStatuses.Active);

                _dataContext.CartItems.Attach(new CartItems
                {
                    Cart = cart,
                    Product = product
                });

                _dataContext.SaveChanges();

                _cacheService.Remove("Cart_" + SessionService.Instance.Account.AccountId);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteFromCart(Guid ProductId)
        {
            try
            {
                var cart = _dataContext.Carts.First(x => x.Account == SessionService.Instance.Account && x.CartStatus == CartStatuses.Active);

                var product = _dataContext.Products.First(x => x.ProductId == ProductId && x.ProductStatus == ProductStatuses.Active);

                _dataContext.CartItems.Remove(new CartItems
                {
                    Cart = cart,
                    Product = product
                });

                _dataContext.SaveChanges();

                _cacheService.Remove("Cart_" + SessionService.Instance.Account.AccountId);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Carts GetCart()
        {
            try
            {
                var cart = _cacheService.Get<Carts>("Cart_" + SessionService.Instance.Account.AccountId);
                if (cart == null)
                {
                    cart = _dataContext.Carts.First(x => x.Account == SessionService.Instance.Account && x.CartStatus == CartStatuses.Active);

                    cart.Account = SessionService.Instance.Account;
                    cart.CartItems = _dataContext.CartItems.Where(x => x.Cart == cart).ToList();

                    foreach (var item in cart.CartItems)
                    {
                        item.Product = _dataContext.Products.First(x => x.ProductId == item.ProductId);
                    }

                    _cacheService.Set<Carts>("Cart_" + SessionService.Instance.Account.AccountId, cart, TimeSpan.FromMinutes(30));
                }

                return cart;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool CompleteCart()
        {
            var cart = GetCart();

            _queueService.Send("Order", cart);

            return true;
        }
    }

    public interface ICartService
    {
        public Carts GetCart();
        public bool AddToCart(Guid ProductId);
        public bool DeleteFromCart(Guid ProductId);
        public bool CompleteCart();

    }
}
