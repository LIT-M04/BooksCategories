using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BooksCategories.Data;
using BooksCategories.Models;

namespace BooksCategories.Controllers
{
    public class BooksController : Controller
    {
        public ActionResult Index(int? categoryId)
        {
            var manager = new BooksManager(Properties.Settings.Default.ConStr);
            IndexViewModel viewModel = new IndexViewModel();
            viewModel.Books = manager.GetBooks(categoryId);
            viewModel.Categories = manager.GetCategories();
            viewModel.CategoryId = categoryId;
            if (TempData["message"] != null)
            {
                viewModel.Message = (string) TempData["message"];
            }
            return View(viewModel);
        }

        public ActionResult NewCategory()
        {
            var manager = new BooksManager(Properties.Settings.Default.ConStr);
            var viewModel = new NewCategoryViewModel();
            viewModel.CategoryList = String.Join(",", manager.GetCategories().Select(c => c.Name));
            return View(viewModel);
        }

        public ActionResult New()
        {
            var manager = new BooksManager(Properties.Settings.Default.ConStr);
            NewViewModel viewModel = new NewViewModel();
            viewModel.Categories = manager.GetCategories();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SaveCategory(string name)
        {
            var manager = new BooksManager(Properties.Settings.Default.ConStr);
            manager.AddCategory(name);
            return Redirect("/books/index");
        }

        [HttpPost]
        public ActionResult SaveBook(string title, string author, int yearPublished, int pageCount,
            int categoryId)
        {
            var manager = new BooksManager(Properties.Settings.Default.ConStr);
            int id = manager.AddBook(new Book
            {
                Author = author,
                Title = title,
                YearPublished = yearPublished,
                PageCount = pageCount
            }, categoryId);
            TempData["message"] = "New Book added, id: " + id;
            return Redirect("/books/index");
        }

    }
}
