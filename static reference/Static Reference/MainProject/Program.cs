using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject
{
    class Program
    {




        static void Main(string[] args)
        {
            People people = new People();

            StaticHelper.initialPeopleObject(people.PeopleList);

            people.addPerson();

            Console.WriteLine("Original list: ");
            foreach (var t in people.PeopleList) Console.WriteLine(t);

            Console.WriteLine("Static reference copy: ");

            StaticHelper.printPeople();

            Console.WriteLine("Change static reference: ");

            StaticHelper.PeopleReference.Add(new Person() { Name = "new dummy person", Surname = "static person", ID = 4 });

            Console.WriteLine(); 
            Console.WriteLine("Static reference: ");

            StaticHelper.printPeople();

            Console.WriteLine("Original list: ");
            foreach (var t in people.PeopleList) Console.WriteLine(t);





            Console.ReadKey(); 

            
        }
    }

    public class People
    {
        public List<Person> PeopleList { get; set;  }

        public People()
        {
            initial(); 
        }
       private void initial()
        {
            PeopleList = new List<Person>()
            {
                new Person(){ID=1 , Name="Person 1" , Surname="MySurname 1" },
                new Person(){ID=1 , Name="Person 2" , Surname="MySurname 2" }

            }; 

        }
        public void addPerson()
        {
            PeopleList.Add(new Person { Name = "dummy person", Surname = "Surname ", ID = 3 }); 
        }



    }

    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int ID { get; set; }

        public override string ToString()
        {
            return $"Name : {Name} , Surname: {Surname} , ID : {ID} "; 
        }
    }
}
