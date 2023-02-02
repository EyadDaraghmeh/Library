using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Library.Controllers
{
    public class LoginController : Controller
    {
        private readonly LibraryContext context;
        public LoginController(LibraryContext c)
        {
            context = c;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Check(Customers custome)
        {
            if(custome.Email.Equals("admin@gmail.com"))
            {
                if(custome.Password.Equals("1410"))
                {
                    return View("Admin");
                }
            }
            List<Customers> customers=context.Customers.ToList();
            foreach (Customers c in customers)
            {
                if(custome.Email.Equals(c.Email))
                {
                    if(c.Password.Equals(custome.Password))
                    {
                        HttpContext.Session.SetInt32("CustomerId", c.Id);
                        return RedirectToAction("Index","Home");
                    }
                }
            }
            return View("Index");
        }
        [HttpPost]
        public IActionResult AddCustomer(Customers customers)
        {
            Customers addcustomer = new Customers() { Name=customers.Name,Email=customers.Email,Password=customers.Password};
            context.Customers.Add(addcustomer);
            context.SaveChanges();
            return View("Index");
        }
        [HttpPost]
        public IActionResult AddBook(Books books)
        {
            Books addbook = new Books() { Name = books.Name, subject = books.subject, Author = books.Author };
            context.Books.Add(addbook);
            context.SaveChanges();
            return View("Admin");

        }
        public IActionResult BooksDetail()
        {
            List<Books> allBooks = context.Books.ToList();
            ViewBag.allBooks = allBooks;
            return View();
        }
    }
}
