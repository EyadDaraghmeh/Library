using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly LibraryContext context;
        public HomeController(LibraryContext c)
        {
            context = c;
        }
        private bool CheckLogin()
        {
            return HttpContext.Session.GetInt32("CustomerId") != null;
        }
        
        public  IActionResult Index()
        {
            if(!CheckLogin()) return RedirectToAction("Index","Login");
            int CustomerId = (int)HttpContext.Session.GetInt32("CustomerId");
            List<CustomersBooks> customerBooks = context.CustomersBooks.Where(e=> e.CustomersId== CustomerId).ToList();
            List<Books> books=new List<Books>();
            List<Books> allBooks= context.Books.ToList();

            foreach (CustomersBooks customer in customerBooks)
            {
                foreach(Books book in allBooks)
                {
                    if(customer.BooksId==book.Id)
                    {
                        books.Add(book);
                    }
                }
            }
            ViewBag.Books = books;
            Customers customers = context.Customers.Where(e => e.Id==CustomerId).ToList().First();
            ViewBag.Customers = customers.Name;

            return View();
        }
        public IActionResult AddBook()
        {
            return View();
        }
        public IActionResult EditBooks()
        {
            List<Books> allBooks = context.Books.ToList();
            ViewBag.allBooks = allBooks;
            return View();
        }
        [HttpPost]
        public IActionResult AddCustomerBooks(CustomersBooks customersBooks)
        {
            int customerId = (int)HttpContext.Session.GetInt32("CustomerId");
            CustomersBooks addCustomerBook= new CustomersBooks() { BooksId=customersBooks.BooksId,CustomersId=customerId};
            context.CustomersBooks.Add(addCustomerBook);
            context.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult DeleteBook()
        {

            int CustomerId = (int)HttpContext.Session.GetInt32("CustomerId");

            List<CustomersBooks> customerBooks = context.CustomersBooks.Where(e => e.CustomersId == CustomerId).ToList();
            List<Books> books = new List<Books>();
            List<Books> allBooks = context.Books.ToList();

            foreach (CustomersBooks customer in customerBooks)
            {
                foreach (Books book in allBooks)
                {
                    if (customer.BooksId == book.Id)
                    {
                        books.Add(book);
                    }
                }
            }
            ViewBag.Books = books;
            return View();
        }
        [HttpPost]
        public IActionResult Dell(CustomersBooks customerBooks)
        {
            int customerId = (int)HttpContext.Session.GetInt32("CustomerId");
            CustomersBooks dellCustomerBook = new CustomersBooks() { BooksId = customerBooks.BooksId, CustomersId = customerId };
            context.CustomersBooks.Remove(dellCustomerBook);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index","Login");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
