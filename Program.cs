using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Teacher
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string ClassSection { get; set; }

    public override string ToString()
    {
        return $"ID: {ID}, Name: {Name}, Class and Section: {ClassSection}";
    }
}


namespace Rainbow_school
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "F:\\Assessment 2\\Rainbow school\\Rainbow school\\Teachers.txt";

            while (true)
            {
                Console.WriteLine("1.Add Teacher\t2.Display Teachers\t3.Update Teacher\t4.Exit");
                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddTeacher(filePath);
                        break;
                    case 2:
                        ViewTeachers(filePath);
                        break;
                    case 3:
                        UpdateTeacher(filePath);
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void AddTeacher(string filePath)
        {
            Console.WriteLine("Enter Teacher Details:");
            Console.Write("ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Class and Section: ");
            string classSection = Console.ReadLine();

            Teacher newTeacher = new Teacher { ID = id, Name = name, ClassSection = classSection };

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{newTeacher.ID},{newTeacher.Name},{newTeacher.ClassSection}");
            }

            Console.WriteLine("Teacher added successfully!");
        }

        static void ViewTeachers(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("No teacher data found.");
                return;
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    Teacher teacher = new Teacher
                    {
                        ID = Convert.ToInt32(parts[0]),
                        Name = parts[1],
                        ClassSection = parts[2]
                    };
                    Console.WriteLine(teacher);
                }
            }
        }

        static void UpdateTeacher(string filePath)
        {
            Console.Write("Enter the ID of the teacher to update: ");
            int targetID = Convert.ToInt32(Console.ReadLine());

            string[] lines = File.ReadAllLines(filePath);
            bool found = false;

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                int currentID = Convert.ToInt32(parts[0]);

                if (currentID == targetID)
                {
                    Console.WriteLine($"Current Details: {lines[i]}");

                    Console.Write("Enter new ID: ");
                    int newID= Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter new Name: ");
                    string newName = Console.ReadLine();
                    Console.Write("Enter new Class and Section: ");
                    string newClassSection = Console.ReadLine();

                    lines[i] = $"{newID},{newName},{newClassSection}";
                    found = true;
                    Console.WriteLine("Teacher updated successfully!");
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("Teacher not found with the specified ID.");
            }

            File.WriteAllLines(filePath, lines);
        }
    }
}
