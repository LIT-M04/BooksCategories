using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksCategories.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int YearPublished { get; set; }
        public int PageCount { get; set; }
        public string CategoryName { get; set; }
    }

    public class BooksManager
    {
        private string _connectionString;

        public BooksManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Book> GetBooks(int? categoryId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT b.*, c.name from Books b JOIN categories c ON " +
                                      "c.id = b.categoryid";
                if (categoryId != null)
                {
                    command.CommandText += " WHERE c.id = @id";
                    command.Parameters.AddWithValue("@id", categoryId);
                }

                connection.Open();
                List<Book> books = new List<Book>();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book();
                    book.Id = (int)reader["Id"];
                    book.Title = (string)reader["Title"];
                    book.Author = (string)reader["Author"];
                    book.PageCount = (int)reader["PageCount"];
                    book.YearPublished = (int)reader["YearPublished"];
                    book.CategoryName = (string)reader["Name"];
                    books.Add(book);
                }

                return books;
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Categories";
                connection.Open();
                List<Category> categories = new List<Category>();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    categories.Add(new Category
                    {
                        Id = (int)reader["id"],
                        Name = (string)reader["Name"]
                    });
                }

                return categories;
            }
        }

        public int AddBook(Book book, int categoryId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Books (Title, Author, YearPublished, PageCount, CategoryId) " +
                                      "VALUES (@title, @author, @yearPublished, @pageCount, @catId); select @@identity;";
                command.Parameters.AddWithValue("@title", book.Title);
                command.Parameters.AddWithValue("@author", book.Author);
                command.Parameters.AddWithValue("@yearPublished", book.YearPublished);
                command.Parameters.AddWithValue("@pageCount", book.PageCount);
                command.Parameters.AddWithValue("@catId", categoryId);

                connection.Open();
                return (int)(decimal)command.ExecuteScalar();
            }
        }

        public void AddCategory(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Categories (Name) " +
                                      "VALUES (@name)";
                command.Parameters.AddWithValue("@name", name);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
