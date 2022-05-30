using DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DbConnection.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Helpers
{
    public class SessionService
    {
        public static DataContext _dataContext = new DataContext();
        private static SessionService _instance = new SessionService();

        public static SessionService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SessionService();
                }
                return _instance;
            }
        }

        public bool IsLogin
        {
            get { return _instance != null; }
        }

        public Accounts Account
        {
            get
            {
                var account = LocalCacheService.GetObjectFromCache<Accounts>("73bad189-7509-4bdc-8858-99e84868a557");
                if (account == null)
                {
                    //Authentication cookie silinmemiş olmasına rağmen cacheler silinmiş olabiliyor.
                    //Böyle durumda Account objesini cache'e tekrar atmak gerek.

                    //return Reload();
                }
                return account;
            }
        }

        public static void DefineAccount(Guid AccountId)
        {
            var account = _dataContext.Accounts.First(x => x.AccountId == AccountId);

            SetAccount(account);
        }

        public static void SetAccount(Accounts account)
        {
            LocalCacheService.AddObjectToCache("73bad189-7509-4bdc-8858-99e84868a557", account, 9999);
        }

        public static void Logout()
        {
            LocalCacheService.RemoveObjectFromCache("73bad189-7509-4bdc-8858-99e84868a557");
            _instance = null;
        }

    }
}
