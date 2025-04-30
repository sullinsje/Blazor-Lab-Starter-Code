namespace Blazor_Lab_Starter_Code {
	class Program
	{

		static void Main()
		{
            Methods m = new Methods();

			m.ReadBooks();
			m.ReadUsers();

			string option;

			do
			{
				Console.WriteLine("Library Management System");
				Console.WriteLine("1. Manage Books");
				Console.WriteLine("2. Manage Users");
				Console.WriteLine("3. Borrow Book");
				Console.WriteLine("4. Return Book");
				Console.WriteLine("5. List Borrowed Books");
				Console.WriteLine("6. Exit");

				Console.Write("Choose an option: ");
				option = Console.ReadLine();

				switch (option)
				{
					case "1":
						ManageBooks(m);
						break;
					case "2":
						ManageUsers(m);
						break;
					case "3":
						//m.BorrowBook();
						break;
					case "4":
						//m.ReturnBook();
						break;
					case "5":
						m.ListBorrowedBooks();
						break;
				}
			} while (option != "6");
		}


		static void ManageBooks(Methods m)
		{

			string option;

			do
			{
				Console.WriteLine("\nManage Books");
				Console.WriteLine("1. Add Book");
				Console.WriteLine("2. Edit Book");
				Console.WriteLine("3. Delete Book");
				Console.WriteLine("4. List Books");
				Console.WriteLine("5. Back");
				Console.Write("Choose an option: ");
				option = Console.ReadLine();

				switch (option)
				{
					case "1":
						//m.AddBook();
						break;
					case "2":
						//m.EditBook();
						break;
					case "3":
						//m.DeleteBook();
						break;
					case "4":
						//m.ListBooks();
						break;
				}
			} while (option != "5");
		}

		static void ManageUsers(Methods m)
		{

			string option;

			do
			{
				Console.WriteLine("\nManage Users");
				Console.WriteLine("1. Add User");
				Console.WriteLine("2. Edit User");
				Console.WriteLine("3. Delete User");
				Console.WriteLine("4. List Users");
				Console.WriteLine("5. Back");

				Console.Write("Choose an option: ");
				option = Console.ReadLine();

				switch (option)
				{
					case "1":
						//m.AddUser();
						break;
					case "2":
						//m.EditUser();
						break;
					case "3":
						//m.DeleteUser();
						break;
					case "4":
						//m.ListUsers();
						break;
				}
			} while (option != "5");
		}

	}
}