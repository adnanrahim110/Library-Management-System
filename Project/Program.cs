using System;
using System.IO;

namespace Project
{
    internal class Program
    {
        private static string dataFilePath = "D:\\project\\LibraryData.txt";
        private static string bookId = null;
        private static string stdId = null;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine
                (
                    "\r\n.____    ._______________________    _____ _______________.___.\r\n|    |   |   \\______   \\______   \\  /  _  \\\\______   \\__  |   |\r\n|    |   |   ||    |  _/|       _/ /  /_\\  \\|       _//   |   |\r\n|    |___|   ||    |   \\|    |   \\/    |    \\    |   \\\\____   |\r\n|_______ \\___||______  /|____|_  /\\____|__  /____|_  // ______|\r\n        \\/           \\/        \\/         \\/       \\/ \\/       \r\n"
                );
                Console.WriteLine("\n\n\t-- MAIN MENU --\n");
                Console.WriteLine("\t1 - LOGIN.");
                Console.WriteLine("\t2 - REGISTER.");
                Console.WriteLine("\t3 - EXIT.");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("\n\n\tEnter your Choice: ");
                Console.ResetColor();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        if (Login())
                        {
                            LibraryMenu();
                        }
                        break;
                    case "2":
                        Register();
                        break;
                    case "3":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nWe are Exiting.. BYE...");
                        Console.ResetColor();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Number");
                        break;
                }
                Console.ReadKey();
            }
        }

        static void LibraryMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n\n\t\t\t\t-----------------------------------------");
                Console.WriteLine("\n\t\t\t\t\t** LIBRARY MANAGEMENT **");
                Console.WriteLine("\n\t\t\t\t-----------------------------------------");
                Console.WriteLine("\n\n\t-- MAIN MENU --\n");

                Console.WriteLine("\t1 - ADD BOOK.");
                Console.WriteLine("\t2 - ADD STUDENT.");
                Console.WriteLine("\t3 - SHOW ALL BOOKS.");
                Console.WriteLine("\t4 - SEARCH BOOK.");
                Console.WriteLine("\t5 - SEARCH STUDENT.");
                Console.WriteLine("\t6 - ISSUE BOOK.");
                Console.WriteLine("\t7 - RETURN BOOK.");
                Console.WriteLine("\t8 - LOGOUT.");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("\n\n\tEnter your Choice: ");
                Console.ResetColor();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        addBook();
                        break;
                    case "2":
                        addStudent();
                        break;
                    case "3":
                        showAllBooks();
                        break;
                    case "4":
                        searchBook();
                        break;
                    case "5":
                        searchStudent();
                        break;
                    case "6":
                        issueBook();
                        break;
                    case "7":
                        returnBook();
                        break;
                    case "8":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nLogging out...");
                        Console.ResetColor();
                        return;
                    default:
                        Console.WriteLine("Invalid Number");
                        break;
                }
                Console.ReadKey();
            }
        }

        static bool Login()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\t\t-- LOGIN --");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\t---------------------------\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tEnter your username: ");
            Console.ResetColor();
            string username = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tEnter your password: ");
            Console.ResetColor();
            string password = Console.ReadLine();

            if (File.Exists(dataFilePath))
            {
                using (StreamReader reader = new StreamReader(dataFilePath))
                {
                    string line;
                    bool inUserSection = false;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line == "[USERS]")
                        {
                            inUserSection = true;
                        }
                        else if (line == "[BOOKS]" || line == "[STUDENTS]" || line == "[ISSUED_BOOKS]")
                        {
                            inUserSection = false;
                        }

                        if (inUserSection)
                        {
                            string[] credentials = line.Split(',');
                            if (credentials[0] == username && credentials[1] == password)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nLogin successful!");
                                Console.ResetColor();
                                return true;
                            }
                        }
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nInvalid username or password.");
            Console.ResetColor();
            return false;
        }

        static void Register()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\t\t-- REGISTER --");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\t---------------------------\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tSet a username: ");
            Console.ResetColor();
            string username = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tSet a password: ");
            Console.ResetColor();
            string password = Console.ReadLine();

            using (StreamWriter writer = new StreamWriter(dataFilePath, true))
            {
                writer.WriteLine("[USERS]");
                writer.WriteLine($"{username},{password}");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nRegistration successful!");
            Console.ResetColor();
        }

        static void addBook()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\t-- ADD BOOK --");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("---------------------------\n");
            Console.ResetColor();

            string bookId = GenerateBookId();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\tGenerated Book ID: {bookId}\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tEnter the Book Name: ");
            Console.ResetColor();
            string bookname = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tEnter Author's Name: ");
            Console.ResetColor();
            string author = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tEnter the Copies of Book: ");
            Console.ResetColor();
            int copies = int.Parse(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tEnter the Publication Year: ");
            Console.ResetColor();
            int publishyear = int.Parse(Console.ReadLine());

            SaveBookData(bookId, bookname, author, copies, publishyear);

            Console.ReadKey();
        }

        static void addStudent()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\t-- ADD STUDENT --");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("---------------------------\n");
            Console.ResetColor();

            string studentId = GenerateStudentId();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\tGenerated Student ID: {studentId}\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tEnter the Student's Name: ");
            Console.ResetColor();
            string stdname = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tEnter Student's Email: ");
            Console.ResetColor();
            string stdemail = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tEnter Student's phone number: ");
            Console.ResetColor();
            long stdnumber = long.Parse(Console.ReadLine());

            SaveStudentData(studentId, stdname, stdemail, stdnumber);

            Console.ReadKey();
        }

        static void showAllBooks()
        {
            Console.Clear();
            if (!File.Exists(dataFilePath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("## No Book information found. ##");
                Console.ResetColor();
                return;
            }

            using (StreamReader Allbooks = new StreamReader(dataFilePath))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n\t-- ALL BOOKS DATA --");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("-----------------------------\n");
                Console.ResetColor();

                string row;
                bool inBookSection = false;
                while ((row = Allbooks.ReadLine()) != null)
                {
                    if (row == "[BOOKS]")
                    {
                        inBookSection = true;
                        continue;
                    }
                    else if (row == "[STUDENTS]" || row == "[USERS]" || row == "[ISSUED_BOOKS]")
                    {
                        inBookSection = false;
                    }

                    if (inBookSection)
                    {
                        string[] data = row.Split(',');
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\nBOOK ID: {data[0]}");
                        Console.WriteLine($"\tName: {data[1]}");
                        Console.WriteLine($"\tAuthor: {data[2]}");
                        Console.WriteLine($"\tTotal Copies: {data[3]}");
                        Console.WriteLine($"\tPublication Year: {data[4]}\n");
                        Console.ResetColor();
                    }
                }
            }
        }

        static void searchBook()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\t-- SEARCH BOOK --");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("---------------------------\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tEnter the Book ID or Book Name: ");
            Console.ResetColor();
            string search = Console.ReadLine().ToLower();

            if (File.Exists(dataFilePath))
            {
                using (StreamReader reader = new StreamReader(dataFilePath))
                {
                    string line;
                    bool inBookSection = false;
                    bool found = false;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line == "[BOOKS]")
                        {
                            inBookSection = true;
                            continue;
                        }
                        else if (line == "[STUDENTS]" || line == "[USERS]" || line == "[ISSUED_BOOKS]")
                        {
                            inBookSection = false;
                        }

                        if (inBookSection)
                        {
                            string[] data = line.Split(',');
                            if (data[0].ToLower() == search || data[1].ToLower() == search)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"\nBOOK ID: {data[0]}");
                                Console.WriteLine($"\tName: {data[1]}");
                                Console.WriteLine($"\tAuthor: {data[2]}");
                                Console.WriteLine($"\tTotal Copies: {data[3]}");
                                Console.WriteLine($"\tPublication Year: {data[4]}\n");
                                Console.ResetColor();
                                found = true;
                                break;
                            }
                        }
                    }

                    if (!found)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nBook not found.");
                        Console.ResetColor();
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNo books data found.");
                Console.ResetColor();
            }
        }

        static void searchStudent()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\t-- SEARCH STUDENT --");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("---------------------------\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tEnter the Student ID or Student Name: ");
            Console.ResetColor();
            string search = Console.ReadLine().ToLower();

            if (File.Exists(dataFilePath))
            {
                using (StreamReader reader = new StreamReader(dataFilePath))
                {
                    string line;
                    bool inStudentSection = false;
                    bool found = false;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line == "[STUDENTS]")
                        {
                            inStudentSection = true;
                            continue;
                        }
                        else if (line == "[BOOKS]" || line == "[USERS]" || line == "[ISSUED_BOOKS]")
                        {
                            inStudentSection = false;
                        }

                        if (inStudentSection)
                        {
                            string[] data = line.Split(',');
                            if (data[0].ToLower() == search || data[1].ToLower() == search)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"\nSTUDENT ID: {data[0]}");
                                Console.WriteLine($"\tName: {data[1]}");
                                Console.WriteLine($"\tEmail: {data[2]}");
                                Console.WriteLine($"\tPhone Number: {data[3]}\n");
                                Console.ResetColor();
                                found = true;
                                break;
                            }
                        }
                    }

                    if (!found)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nStudent not found.");
                        Console.ResetColor();
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNo students data found.");
                Console.ResetColor();
            }
        }

        static void issueBook()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\t-- ISSUE BOOK --");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("---------------------------\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tEnter Student ID: ");
            Console.ResetColor();
            string studentId = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tEnter Book ID: ");
            Console.ResetColor();
            string book_ID = Console.ReadLine();

            if (File.Exists(dataFilePath))
            {
                if (studentId == stdId && book_ID == bookId)
                {
                    using (StreamWriter writer = new StreamWriter(dataFilePath, true))
                    {
                        writer.WriteLine("[ISSUED_BOOKS]");
                        writer.WriteLine($"{studentId},{bookId},{DateTime.Now},ISSUED");
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nBook issued successfully!");
            Console.ResetColor();
        }

        static void returnBook()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\t-- RETURN BOOK --");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("---------------------------\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tEnter Student ID: ");
            Console.ResetColor();
            string studentId = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tEnter Book ID: ");
            Console.ResetColor();
            string book_ID = Console.ReadLine();

            if (File.Exists(dataFilePath))
            {
                if (studentId == stdId && book_ID == bookId)
                {
                    using (StreamWriter writer = new StreamWriter(dataFilePath, true))
                    {
                        writer.WriteLine("[ISSUED_BOOKS]");
                        writer.WriteLine($"{studentId},{bookId},{DateTime.Now},RETURNED");
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nBook returned successfully!");
            Console.ResetColor();
        }

        static void SaveBookData(string bookId, string bookname, string author, int copies, int publishyear)
        {
            using (StreamWriter writer = new StreamWriter(dataFilePath, true))
            {
                writer.WriteLine("[BOOKS]");
                writer.WriteLine($"{bookId},{bookname},{author},{copies},{publishyear}");
            }
        }

        static void SaveStudentData(string studentId, string stdname, string stdemail, long stdnumber)
        {
            using (StreamWriter writer = new StreamWriter(dataFilePath, true))
            {
                writer.WriteLine("[STUDENTS]");
                writer.WriteLine($"{studentId},{stdname},{stdemail},{stdnumber}");
            }
        }

        static string GenerateBookId()
        {
            int bookIdCounter = File.ReadAllLines(dataFilePath).Length / 2;
            bookId = "BK" + bookIdCounter.ToString("D3");
            return bookId;
        }

        static string GenerateStudentId()
        {
            int stdIdCounter = File.ReadAllLines(dataFilePath).Length / 4;
            stdId = "STD" + stdIdCounter.ToString("D3");
            return stdId;
        }
    }
}
