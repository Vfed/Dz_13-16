using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Dz_13_16
{
    class Program
    {


        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }
        }
        static void Main(string[] args)
        {
            string path = @$"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent}\data.txt";
            string path2 = @$"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent}\data2.txt";

            string[] Names = new string[] { "Anton", "Andriy", "Victor", "Oleg", "Ira", "Kate", "Vira", "Kolia" };
            List<Person> people = new List<Person>(); 
            using (StreamWriter sw = new StreamWriter(path))
            {
                Random r = new Random();
                for (int i = 0; i < 100; i++)
                {
                    sw.WriteLine(Names[r.Next(Names.Length)] + " " + r.Next(100));
                }
            }
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] splited;
                    splited = line.Split(' ');
                    if (splited != null && splited.Length == 2)
                    {
                        string name = splited[0];
                        int age = Int32.Parse(splited[1]);
                        people.Add(new Person(name, age));
                    }
                }
            }
            var groupedByAge = people.OrderBy(x => x.Age).GroupBy(x => x.Age);
            var peopleRareAge = groupedByAge.Where(x => x.Count() == groupedByAge.Min(y => y.Count()) ).Select(x => x.Key).ToList();
            var peopleOftenAge = groupedByAge.Where(x => x.Count() == groupedByAge.Max(y => y.Count())).Select(x => x.Key).ToList();
            int middle = 0;
            foreach (var item in groupedByAge)
            {
                middle += item.Key * item.Count();
            }
            middle /= people.Count();
            int inBetwen = people.Where(x => x.Age < 14 || x.Age > 79).Count();
            var notBetwenNames = people.Where(x => !(x.Age < 14 || x.Age > 79)).GroupBy(x => x.Name).Select(x => x.Key).ToList();
            using (StreamWriter sw = new StreamWriter(path2))
            {
                sw.Write("\nRare age: ");
                foreach (var item in peopleRareAge)
                {
                    sw.Write(item + " ");
                }
                sw.Write("\nOften age: ");
                foreach (var item in peopleOftenAge)
                {
                    sw.Write(item + " ");
                }
                sw.Write("\nMiddle age: " + middle);
                sw.Write("\nNumber of People, age not betwen (14-79): " + inBetwen);
                sw.Write("\nNames of People, age betwen (14-79): ");
                foreach (var item in notBetwenNames)
                {
                    sw.Write(item + " ");
                }
            }
        }
    }
}