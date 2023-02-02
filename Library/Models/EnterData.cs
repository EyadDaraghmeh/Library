using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace Library.Models
{
    public static class EnterData
    {
        public static void ADD(LibraryContext context)
        {
            context.Database.EnsureCreated();
            if(context.Customers.Any())
            {
                return;
            }
            Customers customers = new Customers() { Name = "Eyad", Email = "eyad@gmail.com" ,Password="1234"};
            Customers customers1 = new Customers() { Name = "Mohammed", Email = "mohammed@gmail.com", Password = "12345" };
            Customers customers2 = new Customers() { Name = "Ahmad", Email = "ahmad@gmail.com", Password = "123456" };
            context.Customers.Add(customers);
            context.Customers.Add(customers1);
            context.Customers.Add(customers2);
            context.SaveChanges();

            Books books = new Books() {Name="Game Of Thronse" ,subject= "Action & Adventure" ,Author= "George R. R. Martin" };
            Books books1 = new Books() { Name = "IT", subject = "Horror", Author = "A. L. James" };
            Books books2 = new Books() { Name = "Cinquante nuances de grey", subject = "Fiction", Author = "Stephen King" };
            context.Books.Add(books);
            context.Books.Add(books1);
            context.Books.Add(books2);
            context.SaveChanges();

            Customers[] addcustomer= context.Customers.ToArray();
            Books[] addbook=context.Books.ToArray();

            CustomersBooks customersBooks = new CustomersBooks() { CustomersId = addcustomer[0].Id, BooksId = addbook[0].Id };
            CustomersBooks customersBooks1 = new CustomersBooks() { CustomersId = addcustomer[1].Id, BooksId = addbook[0].Id };
            CustomersBooks customersBooks2 = new CustomersBooks() { CustomersId = addcustomer[2].Id, BooksId = addbook[0].Id };
            CustomersBooks customersBooks3 = new CustomersBooks() { CustomersId = addcustomer[0].Id, BooksId = addbook[1].Id };
            CustomersBooks customersBooks4 = new CustomersBooks() { CustomersId = addcustomer[2].Id, BooksId = addbook[2].Id };
            CustomersBooks customersBooks5 = new CustomersBooks() { CustomersId = addcustomer[0].Id, BooksId = addbook[2].Id };

            context.CustomersBooks.Add(customersBooks);
            context.CustomersBooks.Add(customersBooks1);
            context.CustomersBooks.Add(customersBooks2);
            context.CustomersBooks.Add(customersBooks3);
            context.CustomersBooks.Add(customersBooks4);
            context.CustomersBooks.Add(customersBooks5);
            context.SaveChanges();


        }
    }
}
