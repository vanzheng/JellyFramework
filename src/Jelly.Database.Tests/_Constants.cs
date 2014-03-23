
namespace Jelly.Database.Tests
{
    internal static class Constants
    {
        public const string InsertCustomersSqlStatement = "INSERT INTO Customers(CompanyName, ContactName, ContactTitle, Address, Phone, Fax, InsertDate, UpdateDate) VALUES(@CompanyName, @ContactName, @ContactTitle, @Address, @Phone, @Fax, @InsertDate, @UpdateDate)";
    }
}
