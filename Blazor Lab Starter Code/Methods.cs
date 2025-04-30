using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Blazor_Lab_Starter_Code
{
    public class Methods
    {
        public List<Book> books = new List<Book>();
        public List<User> users = new List<User>();
        public Dictionary<User, List<Book>> borrowedBooks = new Dictionary<User, List<Book>>();

        public void ReadBooks()
        {
            try
            {
                foreach (var line in File.ReadLines("./Data/Books.csv"))
                {
                    var fields = line.Split(',');

                    if (fields.Length >= 4)
                    {
                        var book = new Book
                        {
                            Id = int.Parse(fields[0].Trim()),
                            Title = fields[1].Trim(),
                            Author = fields[2].Trim(),
                            ISBN = fields[3].Trim()
                        };

                        books.Add(book);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void ReadUsers()
        {
            try
            {
                foreach (var line in File.ReadLines("./Data/Users.csv"))
                {
                    var fields = line.Split(',');

                    if (fields.Length >= 3) // Ensure there are enough fields
                    {
                        var user = new User
                        {
                            Id = int.Parse(fields[0].Trim()),
                            Name = fields[1].Trim(),
                            Email = fields[2].Trim()
                        };

                        users.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void AddBook(string title, string author, string isbn)
        {
            int id = books.Any() ? books.Max(b => b.Id) + 1 : 1;
            books.Add(new Book { Id = id, Title = title, Author = author, ISBN = isbn });
            Console.WriteLine("Book added successfully!\n");
        }

        public void EditBook(string id, string title, string author, string isbn)
        {

            ListBooks();
            if (int.TryParse(id, out int bookId))
            {
                Book book = books.FirstOrDefault(b => b.Id == bookId);

                if (book != null)
                {
                    if (!string.IsNullOrEmpty(title)) book.Title = title;

                    if (!string.IsNullOrEmpty(author)) book.Author = author;

                    if (!string.IsNullOrEmpty(isbn)) book.ISBN = isbn;

                    Console.WriteLine("Book updated successfully!\n");
                }
                else
                {
                    Console.WriteLine("Book not found!\n");
                }
            }
            else
            {
                Console.WriteLine("Invalid input!");
            }
           
        }

        public void DeleteBook(string id)
        {

            ListBooks();

            if (int.TryParse(id, out int bookId))
            {

                Book book = books.FirstOrDefault(b => b.Id == bookId);

                if (book != null)
                {
                    books.Remove(book);
                    Console.WriteLine("Book deleted successfully!\n");
                }
                else
                {
                    Console.WriteLine("Book not found!\n");
                }
            }
            else
            {
                Console.WriteLine("Invalid input!\n");
            }
        }

        public void ListBooks()
        {

            Console.WriteLine("\nAvailable Books:");

            var bookGroups = books.GroupBy(b => b.Id).Select(bookGroup => new { Book = bookGroup.First(), Count = bookGroup.Count() });

            foreach (var group in bookGroups)
            {
                Console.WriteLine($"{group.Book.Id}. {group.Book.Title} by {group.Book.Author} (ISBN: {group.Book.ISBN}) - Available Copies: {group.Count}");
            }

            Console.WriteLine();
        }

        public void AddUser(string name, string email)
        {
            
            int id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
            users.Add(new User { Id = id, Name = name, Email = email });
            Console.WriteLine("User added successfully!\n");
        }

        public void EditUser(string id, string name, string email)
        {

            ListUsers();

            if (int.TryParse(id, out int userId))
            {

                User user = users.FirstOrDefault(u => u.Id == userId);

                if (user != null)
                {
                    if (!string.IsNullOrEmpty(name)) user.Name = name;

                    if (!string.IsNullOrEmpty(email)) user.Email = email;

                    Console.WriteLine("User updated successfully!\n");
                }
                else
                {
                    Console.WriteLine("User not found!\n");
                }
            }
            else
            {
                Console.WriteLine("Invalid input!\n");
            }
        }

        public void DeleteUser(string id)
        {

            ListUsers();

            if (int.TryParse(id, out int userId))
            {

                User user = users.FirstOrDefault(u => u.Id == userId);

                if (user != null)
                {
                    users.Remove(user);
                    Console.WriteLine("User deleted successfully!\n");
                }
                else
                {
                    Console.WriteLine("User not found!\n");
                }
            }
            else
            {
                Console.WriteLine("Invalid input!\n");
            }
        }

        public void ListUsers()
        {

            Console.WriteLine("\nUsers:");

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Id}. {user.Name} (Email: {user.Email})");
            }

            Console.WriteLine();
        }

        public void BorrowBook(string bid, string uid)
        {

            ListBooks();

            if (int.TryParse(bid, out int bookId))
            {

                Book book = books.FirstOrDefault(b => b.Id == bookId);

                if (book != null && books.Count(b => b.Id == bookId) > 0)
                {

                    ListUsers();

                    if (int.TryParse(uid, out int userId))
                    {

                        User user = users.FirstOrDefault(u => u.Id == userId);

                        if (user != null)
                        {
                            if (!borrowedBooks.ContainsKey(user))
                            {
                                borrowedBooks[user] = new List<Book>();
                            }
                            borrowedBooks[user].Add(book);
                            books.Remove(book);
                            Console.WriteLine("Book borrowed successfully!\n");
                        }
                        else
                        {
                            Console.WriteLine("User not found!\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input!\n");
                    }
                }
                else
                {
                    Console.WriteLine("Book not found or no available copies!\n");
                }
            }
            else
            {
                Console.WriteLine("Invalid input!\n");
            }
        }

        public void ReturnBook(string uid, string bid)
        {

            ListBorrowedBooks();

            if (int.TryParse(uid, out int userId))
            {

                User user = users.FirstOrDefault(u => u.Id == userId);

                if (user != null && borrowedBooks.ContainsKey(user) && borrowedBooks[user].Count > 0)
                {

                    Console.WriteLine("Borrowed Books:");

                    for (int i = 0; i < borrowedBooks[user].Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {borrowedBooks[user][i].Title} by {borrowedBooks[user][i].Author} (ISBN: {borrowedBooks[user][i].ISBN})");
                    }


                    if (int.TryParse(bid, out int bookNumber) && bookNumber >= 1 && bookNumber <= borrowedBooks[user].Count)
                    {

                        Book bookToReturn = borrowedBooks[user][bookNumber - 1];

                        borrowedBooks[user].RemoveAt(bookNumber - 1);
                        books.Add(bookToReturn);

                        Console.WriteLine("Book returned successfully!\n");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input!\n");
                    }
                }
                else
                {
                    Console.WriteLine("User not found or no borrowed books!\n");
                }
            }
            else
            {
                Console.WriteLine("Invalid input!\n");
            }
        }

        public void ListBorrowedBooks()
        {

            Console.WriteLine("\nBorrowed Books:");

            foreach (var entry in borrowedBooks)
            {
                Console.WriteLine($"User: {entry.Key.Name}");

                foreach (var book in entry.Value)
                {
                    Console.WriteLine($"{book.Title} by {book.Author} (ISBN: {book.ISBN})");
                }

                Console.WriteLine();
            }
        }
    }

}

