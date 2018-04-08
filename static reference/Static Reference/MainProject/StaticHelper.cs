using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainProject; 
namespace MainProject
{
    public static class StaticHelper
    {
        public static List<Person> PeopleReference;  
        
        public static void initialPeopleObject(List<Person> p)
        {
            PeopleReference = p; 
        }

        public static void printPeople()
        {
            foreach (var p in PeopleReference)
                Console.WriteLine(p); 
        }

    }
}
