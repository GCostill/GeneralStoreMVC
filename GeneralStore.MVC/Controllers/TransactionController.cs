using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class TransactionController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Transaction
        public ActionResult Index()
        {
            return View(_db.Transactions.ToList());
        }

        //GET: Transaction/{id}
        public ActionResult Details(int id)
        {
            Transaction transaction = _db.Transactions.Find(id);

            if(transaction == null)
            {
                return HttpNotFound();
            }

            return View(transaction);
        }
        //GET: Transaction/Create
        //This uses the transaction view models
        public ActionResult Create()
        {
            var viewModel = new CreateTransactionViewModel();

            viewModel.Customers = _db.Customers.Select(customer => new SelectListItem 
            { 
                Text = customer.FirstName + " " + customer.LastName,
                Value = customer.CustomerID.ToString()
            });

            viewModel.Products = _db.Products.Select(product => new SelectListItem 
            { 
                Text = product.Name,
                Value = product.ProductId.ToString()
            });
            return View();
        }

        //POST: Transaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTransactionViewModel viewModel)
        {
            return View(viewModel);
        }

        //GET: Transaction/Delete/{id}
        public ActionResult Delete(int id)
        {
            Transaction transaction = _db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
                //if desired, you could return a view here to redirect user to error page
            }

            return View(transaction);
        }

        //POST: Transactin/Delete{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(transaction).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        //GET: Transaction/Edit/{id}
        public ActionResult Edit(int id)
        {
            Transaction transaction = _db.Transactions.Find(id);
            if(transaction == null)
            {
                return HttpNotFound();
            }

            ViewData["Customers"] = _db.Customers.Select(customer => new SelectListItem
            {
                Text = customer.FirstName + " " + customer.LastName,
                Value = customer.CustomerID.ToString()
            });

            ViewData["Products"] = _db.Products.Select(product => new SelectListItem
            {
                Text = product.Name,
                Value = product.ProductId.ToString()
            });

            return View(transaction);
        }

        //POST: Transaction/Edit{id}
        [HttpPost]
        public ActionResult Edit(Transaction transaction)
        {
            ViewData["Customers"] = _db.Customers.Select(customer => new SelectListItem
            {
                Text = customer.FirstName + " " + customer.LastName,
                Value = customer.CustomerID.ToString()
            });

            ViewData["Products"] = _db.Products.Select(product => new SelectListItem
            {
                Text = product.Name,
                Value = product.ProductId.ToString()
            });

            return View(transaction);
        }
    }
}