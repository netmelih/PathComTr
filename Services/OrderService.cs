using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using DbConnection;

namespace Services
{
    public class OrderService : IOrderService
    {
        public static DataContext _dataContext;

        public OrderService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


    }

    public interface IOrderService
    {

    }
}
