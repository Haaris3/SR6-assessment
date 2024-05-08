using System;
using System.Collections.Generic;
using System.IO;

namespace SR6
{
    internal class Program
    {
        class Student
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string MobileNumber { get; set; }
            public string Address { get; set; }

            public Student(string firstName, string lastName, string email, string mobileNumber, string address)
            {
                FirstName = firstName;
                LastName = lastName;
                Email = email;
                MobileNumber = mobileNumber;
                Address = address;
            }

            public override string ToString()
            {
                return $"{FirstName} {LastName}, Email: {Email}, Mobile: {MobileNumber}, Address: {Address}";
            }
        }

        static List<Student> studentRecords = new List<Student>();

        static void Main(string[] args)
        {
            LoadFromFile(); // Load existing records from file if any

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("==========Student Information==========");
                Console.WriteLine("1. Add information");
                Console.WriteLine("2. Insert information");
                Console.WriteLine("3. Update information");
                Console.WriteLine("4. Delete information");
                Console.WriteLine("5. Search information by name");
                Console.WriteLine("6. Display information");
                Console.WriteLine("7. Save to file");
                Console.WriteLine("8. Exit");

                Console.Write("Enter your choice: ");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddInformation();
                        break;
                    case 2:
                        InsertInformation();
                        break;
                    case 3:
                        UpdateInformation();
                        break;
                    case 4:
                        DeleteInformation();
                        break;
                    case 5:
                        SearchInformation();
                        break;
                    case 6:
                        DisplayInformation();
                        break;
                    case 7:
                        SaveToFile();
                        break;
                    case 8:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
        }

        static void AddInformation()
        {
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter email: ");
            string email = Console.ReadLine();
            Console.Write("Enter mobile number: ");
            string mobileNumber = Console.ReadLine();
            Console.Write("Enter address: ");
            string address = Console.ReadLine();

            Student student = new Student(firstName, lastName, email, mobileNumber, address);
            studentRecords.Add(student);

            Console.WriteLine("Student information added successfully!");
        }

        static void InsertInformation()
        {
            if (studentRecords.Count == 0)
            {
                Console.WriteLine("No records available to insert. Please add some records first.");
                return;
            }

            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter email: ");
            string email = Console.ReadLine();
            Console.Write("Enter mobile number: ");
            string mobileNumber = Console.ReadLine();
            Console.Write("Enter address: ");
            string address = Console.ReadLine();

            Console.Write("Enter the index to insert (0 to N): ");
            int index;
            if (!int.TryParse(Console.ReadLine(), out index) || index < 0 || index > studentRecords.Count)
            {
                Console.WriteLine("Invalid index. Insertion failed.");
                return;
            }

            Student student = new Student(firstName, lastName, email, mobileNumber, address);
            studentRecords.Insert(index, student);
            Console.WriteLine($"Student information inserted at index {index} successfully!");
        }

        static void UpdateInformation()
        {
            if (studentRecords.Count == 0)
            {
                Console.WriteLine("No records available to update. Please add some records first.");
                return;
            }

            Console.WriteLine("Enter the index of the student you want to update:");
            int index;
            if (!int.TryParse(Console.ReadLine(), out index) || index < 0 || index >= studentRecords.Count)
            {
                Console.WriteLine("Invalid index. Update failed.");
                return;
            }

            Console.WriteLine("Enter new information:");
            Console.Write("Enter email: ");
            string newEmail = Console.ReadLine();
            Console.Write("Enter mobile number: ");
            string newMobileNumber = Console.ReadLine();
            Console.Write("Enter address: ");
            string newAddress = Console.ReadLine();

            Student student = studentRecords[index];
            student.Email = newEmail;
            student.MobileNumber = newMobileNumber;
            student.Address = newAddress;

            Console.WriteLine("Student information updated successfully!");
        }

        static void DeleteInformation()
        {
            if (studentRecords.Count == 0)
            {
                Console.WriteLine("No records available to delete.");
                return;
            }

            Console.WriteLine("Current Student Records:");
            for (int i = 0; i < studentRecords.Count; i++)
            {
                Console.WriteLine($"{i}. {studentRecords[i]}");
            }

            Console.Write("Enter the index of the student you want to delete (0 to N-1): ");
            int index;
            if (!int.TryParse(Console.ReadLine(), out index) || index < 0 || index >= studentRecords.Count)
            {
                Console.WriteLine("Invalid index. Deletion failed.");
                return;
            }

            studentRecords.RemoveAt(index);
            Console.WriteLine($"Student at index {index} deleted successfully.");
        }

        static void SearchInformation()
        {
            if (studentRecords.Count == 0)
            {
                Console.WriteLine("No records available to search.");
                return;
            }

            Console.Write("Enter the name of the student you want to search: ");
            string searchName = Console.ReadLine();

            bool found = false;
            for (int i = 0; i < studentRecords.Count; i++)
            {
                if (studentRecords[i].FirstName.Equals(searchName, StringComparison.OrdinalIgnoreCase) ||
                    studentRecords[i].LastName.Equals(searchName, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Student found at index {i}: {studentRecords[i]}");
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("Student not found.");
        }

        static void DisplayInformation()
        {
            if (studentRecords.Count == 0)
            {
                Console.WriteLine("No records available to display.");
                return;
            }

            Console.WriteLine("Student Records:");
            for (int i = 0; i < studentRecords.Count; i++)
            {
                Console.WriteLine($"{i}. {studentRecords[i]}");
            }
        }

        static void SaveToFile()
        {
            using (StreamWriter writer = new StreamWriter("student_records.txt"))
            {
                foreach (var student in studentRecords)
                {
                    writer.WriteLine($"{student.FirstName},{student.LastName},{student.Email},{student.MobileNumber},{student.Address}");
                }
            }
            Console.WriteLine("Student records saved to file.");
        }

        static void LoadFromFile()
        {
            if (File.Exists("student_records.txt"))
            {
                using (StreamReader reader = new StreamReader("student_records.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 5)
                        {
                            string firstName = parts[0];
                            string lastName = parts[1];
                            string email = parts[2];
                            string mobileNumber = parts[3];
                            string address = parts[4];
                            Student student = new Student(firstName, lastName, email, mobileNumber, address);
                            studentRecords.Add(student);
                        }
                    }
                }
                Console.WriteLine("Student records loaded from file.");
            }
        }
    }
}
