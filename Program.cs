using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
namespace MyReadingApp
{
    class MyFirstConsoleReadingApp
    {
        public class Retrieve {
            public void Display(String filePath, List<String> Lines)
            {
                foreach (var line in Lines)
                {
                    string[] entries = line.Split(",");
                    Console.WriteLine($"{entries[0]} {entries[1]} {entries[2]}");
                }
            }
        }
        public class StoreAdd
        {
            public  void Store(String filePath,List<string> Lines)
            {

                String choice = "Y";
                while (choice == "Y")
                {
                    Console.WriteLine("Enter ID of the Teacher: ");
                    int tempId=Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter Name of the Teacher: ");
                    String tempName= Console.ReadLine();
                    Console.WriteLine("Enter the class and section of the Teacher: ");
                    String tempClass_section = Console.ReadLine();
                    Lines.Add($"{ tempId},{ tempName},{ tempClass_section}");
                    File.WriteAllLines(filePath, Lines);
                    Console.WriteLine();
                    Console.WriteLine("Do you want to continue? (Y/N)");
                    choice = Console.ReadLine().ToUpper();

                }
            }
        }
        public class Update_Teacher
        {
            public void Update(String filePath, List<Teacher> Teachers)
            {
                Console.WriteLine("Enter the Id of the Teacher : ");
                int tempId = Convert.ToInt32(Console.ReadLine());
                //string tempId = Console.ReadLine();
                Teacher tempTeacher = Teachers.Where(i => i.Id == tempId).FirstOrDefault();
                if (tempTeacher == null)
                {
                    Console.WriteLine("\n Invalid ID");
                }
                else
                {
                    Console.WriteLine("Enter new Name: ");
                    string tempName = Console.ReadLine();
                    //int choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the Class and Section : ");
                    String tempClass_Section = Console.ReadLine();
                    tempTeacher.Name = tempName;
                    tempTeacher.Class_Section = tempClass_Section;

                    //Deleting all contents of the file
                    File.Delete(filePath);
                    List<String> answer = new List<String>();
                    foreach(var teacher in Teachers)
                    {
                        answer.Add($"{teacher.Id},{teacher.Name},{teacher.Class_Section}");
                    }
                    File.AppendAllLines(filePath, answer);



                }
                

            }
        }
        public static void Main(String[] args)
        {
            
            String filePath = @"E:\Sample.txt";
            List<string> Lines = File.ReadAllLines(filePath).ToList();
            List<Teacher> Teachers = new List<Teacher>();
            foreach (string line in Lines)
            {
                string[] entries = line.Split(',');
                Teacher tempTeacher= new Teacher();
                tempTeacher.Id = Convert.ToInt32(entries[0]);
                tempTeacher.Name = entries[1];
                tempTeacher.Class_Section= entries[2];
                Teachers.Add(tempTeacher);
            }

            Retrieve retrieve = new Retrieve();
            StoreAdd store = new StoreAdd();
            Update_Teacher update_Teacher = new Update_Teacher();
            //Retrieve.Display(filePath,Lines);
            //StoreAdd.Store(filePath,Lines);
            //Update_Teacher.Update(filePath,Lines);
            string u = "Y";
            while (u == "Y")
            {
                int choice = 0;
                Console.WriteLine("Enter No. to Perform operation : \n 1.Display \n 2.Add a Teacher \n 3.Update a Teacher \n 4.Exit");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        retrieve.Display(filePath, Lines);
                        break;
                    case 2:
                        store.Store(filePath, Lines);
                        break;
                    case 3:
                        update_Teacher.Update(filePath, Teachers);
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Enter a proper Option : ");
                        break;
                }
                Console.WriteLine("Want to Continue ?(Y/N) ");
                u = Console.ReadLine().ToUpper();
                
            } 
        }
    }
}
