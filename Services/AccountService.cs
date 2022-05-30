using DbConnection;
using DbConnection.Models;
using Microsoft.AspNetCore.Http;

namespace Services
{
    public class AccountService : IAccountService
    {
        public static DataContext _dataContext;

        public AccountService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Accounts GetAccount(string Username, string Password)
        {
            try
            {
                return _dataContext.Accounts.First(x => x.Username == Username && x.Password == Password);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public interface IAccountService
    {
        public Accounts GetAccount(string UserName, string Password);
    }
}
