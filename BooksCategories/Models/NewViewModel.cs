using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BooksCategories.Data;

namespace BooksCategories.Models
{
    public class NewViewModel
    {
        public IEnumerable<Category> Categories { get; set; } 
    }
}