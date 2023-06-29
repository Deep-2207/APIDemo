using ApiDemo_1.Model;
using Dapper;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ApiDemo_1.Repository
{
    public class ProductRepository
    {
        private string connectionString;
        public ProductRepository()
        {
            connectionString = @"Server=MSI;Database=Product;Trusted_Connection=True;";
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }

        public void Add(Product prod)
        {
            using (IDbConnection IDbConnection = Connection)
            {
                string sQuery = @"INSERT INTO Product (ProductName,Price) values (@ProductName,@Price)";
                IDbConnection.Open();
                IDbConnection.Execute(sQuery,prod);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            using (IDbConnection IDbConnection = Connection)
            {
                string sQuery = @"SELECT * FROM Product";
                IDbConnection.Open();

                return IDbConnection.Query<Product>(sQuery);
            }
        }

        public Product GetByID(int productID)
        {
            using (IDbConnection IDbConnection = Connection)
            {
                string sQuery = @"SELECT * FROM Product WHERE ProductID=@ProductID";
                IDbConnection.Open();

                return IDbConnection.Query<Product>(sQuery, new { ProductID = productID}).FirstOrDefault();
            }
        }

        public void Delete(int productID)
        {
            using (IDbConnection IDbConnection = Connection)
            {
                string sQuery = @"DELETE FROM Product WHERE ProductID=@ProductID";
                IDbConnection.Open();
                IDbConnection.Execute(sQuery, new { ProductID = productID});
            }
        }

        public void Update(Product prod)
        {
            using (IDbConnection IDbConnection = Connection)
            {
                string sQuery = @"UPDATE Product SET ProductName = @ProductName, Price=@Price  WHERE ProductID = @ProductID";
                IDbConnection.Open();
                IDbConnection.Execute(sQuery, prod);
            }
        }
    }
}
