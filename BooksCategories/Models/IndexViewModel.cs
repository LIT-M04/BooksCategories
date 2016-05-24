using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BooksCategories.Data;

namespace BooksCategories.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int? CategoryId { get; set; }
        public string Message { get; set; }
    }
}