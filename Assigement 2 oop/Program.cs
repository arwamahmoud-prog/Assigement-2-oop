using System;

namespace Assignment02OOP
{
    // ==========================================
    // Part 01: Employee Class
    // ==========================================

    public enum Gender { M, F }

    [Flags]
    public enum SecurityPrivilege
    {
        Guest = 1,
        Developer = 2,
        Secretary = 4,
        DBA = 8,
        SecurityOfficer = 15 // 1+2+4+8 (Full permissions)
    }

    public class HireDate
    {
        private int day;
        private int month;
        private int year;

        public int Day
        {
            get { return day; }
            set { day = (value > 0 && value <= 31) ? value : 1; } // No runtime errors
        }
        public int Month
        {
            get { return month; }
            set { month = (value > 0 && value <= 12) ? value : 1; }
        }
        public int Year
        {
            get { return year; }
            set { year = (value >= 1900 && value <= DateTime.Now.Year) ? value : 2000; }
        }

        public HireDate(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }

        public override string ToString() => $"{Day:D2}/{Month:D2}/{Year}";
    }

    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public SecurityPrivilege SecurityLevel { get; set; }
        public double Salary { get; set; }
        public HireDate HireDate { get; set; }
        public Gender EmpGender { get; set; }

        public Employee(int id, string name, SecurityPrivilege security, double salary, HireDate hireDate, Gender gender)
        {
            ID = id;
            Name = name ?? "Unknown";
            SecurityLevel = security;
            Salary = salary >= 0 ? salary : 0;
            HireDate = hireDate ?? new HireDate(1, 1, 2000);
            EmpGender = gender;
        }

        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}, Gender: {EmpGender}, Role: {SecurityLevel}, " +
                   $"Salary: {String.Format("{0:C}", Salary)}, Hired: {HireDate}";
        }
    }

    // ==========================================
    // Part 2: Static Binding (new)
    // ==========================================

    public class Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Shape(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double Area() => Width * Height;

        public override string ToString() => $"(Width = {Width}, Height = {Height})";
    }

    public class Cube : Shape
    {
        public double Depth { get; set; }

        public Cube(double width, double height, double depth) : base(width, height)
        {
            Depth = depth;
        }

        public new double Area() => base.Area() * Depth;

        public void Print() => Console.WriteLine($"(Width = {Width}, Height = {Height}, Depth = {Depth})");
    }

    // ==========================================
    // Part 2: Dynamic Binding (virtual/override)
    // ==========================================

    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public void Greet() => Console.WriteLine("I am a person's basic data.");

        public virtual void Display() => Console.WriteLine($"Person: {Name}, Age: {Age}");
    }

    public class Doctor : Person
    {
        public string Specialty { get; set; }

        public new void Greet() => Console.WriteLine("I am a Doctor.");

        public override void Display() => Console.WriteLine($"Doctor: {Name}, Age: {Age}, Specialty: {Specialty}");
    }

    public class Engineer : Person
    {
        public string Field { get; set; }

        public new void Greet() => Console.WriteLine("I am an Engineer.");

        public override void Display() => Console.WriteLine($"Engineer: {Name}, Age: {Age}, Field: {Field}");
    }

    class Program
    {
        static void ProcessPerson(Person person)
        {
            person.Greet();
            person.Display();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("--- Part 01: Employees ---");
            Employee[] EmpArr = new Employee[3];
            EmpArr[0] = new Employee(1, "Ahmed", SecurityPrivilege.DBA, 5000, new HireDate(15, 6, 2020), Gender.M);
            EmpArr[1] = new Employee(2, "Mona", SecurityPrivilege.Guest, 2000, new HireDate(1, 1, 2023), Gender.F);
            EmpArr[2] = new Employee(3, "Omar", SecurityPrivilege.SecurityOfficer, 8000, new HireDate(10, 10, 2018), Gender.M);

            foreach (var emp in EmpArr)
            {
                Console.WriteLine(emp.ToString());
            }

            Console.WriteLine("\n--- Part 02: Static Binding ---");
            // Q3
            Shape shape = new Shape(2, 3);
            Console.WriteLine($"shape.Area(): {shape.Area()}"); // 6

            Cube cube = new Cube(2, 3, 4);
            Console.WriteLine($"cube.Area(): {cube.Area()}"); // 24

            Shape shapeRef = new Cube(2, 3, 4);
            Console.WriteLine($"shapeRef.Area(): {shapeRef.Area()}"); // 6 (Static binding!)

            // Q4
            object obj = new Cube(1, 2, 3);
            Console.WriteLine(obj.ToString()); // Calls Shape's ToString() polymorphically (Late binding)

            Console.WriteLine("\n--- Part 02: Dynamic Binding ---");
            // Q7
            Person doc = new Doctor { ID = 1, Name = "Dr. Ali", Age = 45, Specialty = "Cardiology" };
            Person eng = new Engineer { ID = 2, Name = "Eng. Sara", Age = 30, Field = "Software" };

            Console.WriteLine("Processing Doctor:");
            ProcessPerson(doc);

            Console.WriteLine("Processing Engineer:");
            ProcessPerson(eng);

            Console.ReadLine();
        }
    }
}
