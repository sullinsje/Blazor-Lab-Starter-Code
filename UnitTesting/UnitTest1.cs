using Blazor_Lab_Starter_Code;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using static System.Reflection.Metadata.BlobBuilder;
using System.Net;

namespace UnitTesting
{
    [TestClass]
    public class ReadBooks
    {
        [TestMethod]
        public void ReadBooksInitiallyEmpty()
        {
            //Arrange
            Methods m = new Methods();
            int count = m.books.Count;

            //Act
            //no action needed, testing initialization

            //Assert
            Assert.AreNotEqual(1000, count);
            Assert.AreEqual(0, m.books.Count);
        }

        [TestMethod]
        public void ReadBooksFillsList()
        {
            //Arrange
            Methods m = new();

            //Act
            m.ReadBooks();

            //Assert
            Assert.AreNotEqual(0, m.books.Count);
            Assert.IsNotNull(m.books[0]);
            Assert.IsNotNull(m.books.Last());
            Assert.AreEqual(1000, m.books.Count);
        }

        [TestMethod]
        public void ReadBooksThrowsOutOfRangeException()
        {
            var m = new Methods();

            m.ReadBooks();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => m.books[-1]);

        }
    }

    [TestClass]
    public class ReadUsers
    {
        [TestMethod]
        public void ReadUsersInitiallyEmpty()
        {
            //Arrange
            Methods m = new Methods();
            int count = m.users.Count;

            //Act
            //no action needed, testing initialization

            //Assert
            Assert.AreNotEqual(1000, count);
            Assert.AreEqual(0, m.users.Count);
        }

        [TestMethod]
        public void ReadUsersFillsList()
        {
            //Arrange
            Methods m = new();

            //Act
            m.ReadUsers();

            //Assert
            Assert.AreNotEqual(0, m.users.Count);
            Assert.IsNotNull(m.users[0]);
            Assert.IsNotNull(m.users.Last());
            Assert.AreEqual(100, m.users.Count);
        }

        [TestMethod]
        public void ReadUsersThrowsOutOfRangeException()
        {
            var m = new Methods();

            m.ReadUsers();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => m.users[-1]);

        }
    }

    [TestClass]
    public class AddBook
    {
        [TestMethod]
        [DataRow("Test", "Test", "Test")]
        [DataRow("qqqqqqqqqqqqqqqqqqqqqq", "wwwwwwwwwwwwwwwwwwwwwwww", "eeeeeeeeeeeeeeee")]
        public void AddBookUpdatesCount(string name, string title, string isbn)
        {
            Methods m = new Methods();
            m.ReadBooks();
            int count = m.books.Count;
            

            m.AddBook(name, title, isbn);

            Assert.AreNotEqual(count, m.books.Count);
            Assert.AreEqual(1001, m.books.Count);
        }

        [TestMethod]
        [DataRow("Test", "Test", "Test")]
        public void AddBookGivesCorrectID(string name, string title, string isbn)
        {
            Methods m = new();

            m.AddBook(name, title, isbn);

            Assert.AreEqual(1, m.books[0].Id);
            Assert.AreNotEqual(0, m.books[0].Id);
        }

    }

    [TestClass]
    public class EditBook
    {
        [TestMethod]
        [DataRow ("1", "TestTitle", "TestAuthor", "TestISBN")]

        [DataRow("1000", "TestTitle", "TestAuthor", "TestISBN")]
        public void EditBookChangesFields(string id, string title, string author, string isbn)
        {
            Methods m = new Methods();
            m.ReadBooks();
            int bookId = int.Parse(id);
            Book book = m.books.FirstOrDefault(b => b.Id == bookId);
            string oldTitle = book.Title;
            string oldAuthor = book.Author;
            string oldISBN = book.ISBN;

            m.EditBook(id, title, author, isbn);
            book = m.books.FirstOrDefault(b => b.Id == bookId);

            Assert.AreNotEqual(oldTitle, book.Title);
            Assert.AreNotEqual(oldISBN, book.ISBN);
            Assert.AreNotEqual(oldAuthor, book.Author);
        }

        [TestMethod]
        [DataRow("1", "Test", "Test", "Test")]
        [DataRow("1000", "TestTitle", "TestAuthor", "TestISBN")]
        public void EditBookHasParameters(string id, string title, string author, string isbn)
        {
            Methods m = new Methods();
            m.ReadBooks();
            int bookId = int.Parse(id);
            
            Book book = m.books.FirstOrDefault(b => b.Id == bookId);

            m.EditBook(id, title, author, isbn);
            book = m.books.FirstOrDefault(b => b.Id == bookId);

            Assert.AreEqual(title, book.Title);
            Assert.AreEqual(isbn, book.ISBN);
            Assert.AreEqual(author, book.Author);
        }


    }

    [TestClass]
    public class DeleteBook
    {
        [TestMethod]
        [DataRow("1")]
        [DataRow("99")]
        [DataRow("50")]
        public void DeleteBookDecrementsCount(string id)
        {
            var m = new Methods();
            m.ReadBooks();
            int count = m.books.Count;

            m.DeleteBook(id);

            Assert.AreNotEqual(count, m.books.Count);
        }

        [TestMethod]
        [DataRow("1")]
        [DataRow("99")]
        [DataRow("50")]
        public void DeleteBookReassignedID(string id)
        {
            var m = new Methods();
            m.ReadBooks();
            int index = int.Parse(id) - 1;
            var b1 = m.books[index];

            m.DeleteBook(id);
            var b2 = m.books[index];

            Assert.AreNotEqual(b1.Title, b2.Title);
            Assert.AreNotEqual(b1.Author, b2.Author);
            Assert.AreNotEqual(b1.ISBN, b2.ISBN);
        }

        
    }

    [TestClass]
    public class AddUser
    {
        [TestMethod]
        [DataRow("Test", "Test")]
        [DataRow("qqqqqqqqqqqqqqqqqqqqqq", "wwwwwwwwwwwwwwwwwwwwwwww")]
        public void AddUserUpdatesCount(string name, string email)
        {
            Methods m = new Methods();
            m.ReadUsers();
            int count = m.users.Count;


            m.AddUser(name, email);

            Assert.AreNotEqual(count, m.users.Count);
            Assert.AreEqual(101, m.users.Count);
        }

        [TestMethod]
        [DataRow("Test", "Test")]
        public void AddUserGivesCorrectID(string name, string email)
        {
            Methods m = new();

            m.AddUser(name, email);

            Assert.AreEqual(1, m.users[0].Id);
            Assert.AreNotEqual(0, m.users[0].Id);
        }

    }

    [TestClass]
    public class EditUser
    {
        [TestMethod]
        [DataRow("1", "Test", "Test")]

        [DataRow("100", "Test", "Test")]
        public void EditUserChangesFields(string id, string name, string email)
        {
            Methods m = new Methods();
            m.ReadUsers();
            int userID = int.Parse(id);
            User user = m.users.FirstOrDefault(b => b.Id == userID);
            string oldName = user.Name;
            string oldEmail = user.Email;

            m.EditUser(id, name, email);
            user = m.users.FirstOrDefault(b => b.Id == userID);

            Assert.AreNotEqual(oldName, user.Name);
            Assert.AreNotEqual(oldEmail, user.Email);
        }

        [TestMethod]
        [DataRow("1", "Test", "Test")]
        [DataRow("100", "Test", "Test")]
        public void EditBookHasParameters(string id, string name, string email)
        {
            Methods m = new Methods();
            m.ReadUsers();
            int userID = int.Parse(id);

            User user = m.users.FirstOrDefault(b => b.Id == userID);


            m.EditUser(id, name, email);
            user = m.users.FirstOrDefault(b => b.Id == userID);

            Assert.AreEqual(name, user.Name);
            Assert.AreEqual(email, user.Email);
        }


    }

    [TestClass]
    public class DeleteUser
    {
        [TestMethod]
        [DataRow("1")]
        [DataRow("97")]
        [DataRow("50")]
        public void DeleteBookDecrementsCount(string id)
        {
            var m = new Methods();
            m.ReadUsers();
            int count = m.users.Count;

            m.DeleteUser(id);

            Assert.AreNotEqual(count, m.users.Count);
        }

        [TestMethod]
        [DataRow("1")]
        [DataRow("97")]
        [DataRow("50")]
        public void DeleteUserReassignedID(string id)
        {
            var m = new Methods();
            m.ReadUsers();
            int index = int.Parse(id) - 1;
            var b1 = m.users[index];

            m.DeleteUser(id);
            var b2 = m.users[index];

            Assert.AreNotEqual(b1.Name, b2.Name);
            Assert.AreNotEqual(b1.Email, b2.Email);
        }



    }

    [TestClass]
    public class BorrowBook
    {
        [TestMethod]
        [DataRow("1", "1")]

        [DataRow("1000", "100")]
        public void BorrowBookDecrementsBookCount(string bookId, string userID)
        {
            var m = new Methods();
            m.ReadUsers();
            m.ReadBooks();

            int count = m.books.Count;

            m.BorrowBook(bookId, userID);

            Assert.AreEqual(count - 1, m.books.Count);
            Assert.AreNotEqual(count, m.books.Count);
        }

        [TestMethod]
        [DataRow("1", "1")]

        [DataRow("1000", "100")]
        public void BorrowBookIncrementsBorrowedBookCount(string bookId, string userID)
        {
            var m = new Methods();
            m.ReadUsers();
            m.ReadBooks();

            int count = m.borrowedBooks.Count;

            m.BorrowBook(bookId, userID);

            Assert.AreEqual(count + 1, m.borrowedBooks.Count);
            Assert.AreNotEqual(count, m.borrowedBooks.Count);
        }
    }

    [TestClass]
    public class ReturnBook
    {
        [TestMethod]
        [DataRow("1", "1")]
        [DataRow("100", "1000")]
        public void ReturnBookDecrementsBorrowedBook(string userId, string bookId)
        {
            var m = new Methods();
            m.ReadUsers();
            m.ReadBooks();
            m.BorrowBook(bookId, userId);

            int userID = int.Parse(userId);
            User user = m.users.FirstOrDefault(b => b.Id == userID);
            int count = m.borrowedBooks[user].Count;

            m.ReturnBook(userId, "1");

            Assert.AreEqual(count - 1, m.borrowedBooks[user].Count);
            Assert.AreNotEqual(count, m.borrowedBooks[user].Count);

        }

        [TestMethod]
        [DataRow("1", "1")]
        [DataRow("100", "1000")]
        public void ReturnBookIncrementsBookCount(string userId, string bookId)
        {

            var m = new Methods();
            m.ReadUsers();
            m.ReadBooks();
            m.BorrowBook(bookId, userId);

            int count = m.books.Count;

            m.ReturnBook(userId, "1");

            Assert.AreEqual(count + 1, m.books.Count);
            Assert.AreNotEqual(count, m.books.Count);
        }
    }
}