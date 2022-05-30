using DbConnection.Models;

namespace DbConnection
{
    public class DbInitializer
    {
        public static DataContext _dataContext = new DataContext();

        public static void Initialize()
        {
            _dataContext.Database.EnsureCreated();

            // Look for any students.
            if (_dataContext.Accounts.Any())
            {
                return;   // DB has been seeded
            }

            var accounts = new Accounts[]
            {
                new Accounts{ AccountDisplayName = "Melih Utku Diker", Password = "123456", Username = "doctor" },
                new Accounts{ AccountDisplayName = "Ayça Diker", Password = "654321", Username = "nurse" }
            };

            _dataContext.Accounts.AddRange(accounts);

            _dataContext.SaveChanges();

            var products = new Products[]
            {
                new Products{ ProductName = "Monopoly", ProductDescription = "Eğlenceli bir masa oyunu", ProductPrice = 83.99M, ProductStatus = ProductStatuses.Active },
                new Products{ ProductName = "LEGO Technic 42115 Lamborghini", ProductDescription = "LEGO Technic 42115 Lamborghini Sián FKP 37 Yapım Seti (3696 Parça) - Çocuk ve Yetişkin için Koleksiyonluk Oyuncak Araba ", ProductPrice = 4799.90M, ProductStatus = ProductStatuses.Active },
                new Products{ ProductName = "MSI GF63 Thin 11UC-616XTR", ProductDescription = "MSI GF63 Thin 11UC-616XTR Intel Core i7 11800H 8GB 512GB SSD RTX3050 Freedos 15.6\" FHD Taşınabilir Bilgisayar", ProductPrice = 16599, ProductStatus = ProductStatuses.Active },
                new Products{ ProductName = "iPhone 11 128 GB ", ProductDescription = "iPhone 11 128 GB", ProductPrice = 13249, ProductStatus = ProductStatuses.Active },
                new Products{ ProductName = "Philips HD9650/90 XXL Avance Collection Airfryer Fritöz", ProductDescription = "Philips HD9650/90 XXL Avance Collection Airfryer Fritöz", ProductPrice = 4259.89M, ProductStatus = ProductStatuses.Active },
            };

            _dataContext.Products.AddRange(products);

            _dataContext.SaveChanges();
        }
    }
}
