using System;
using System.Collections.Generic;
using System.Linq; 
namespace ConsoleApp2
{
    class Test
    {
        public int? WMS_ID { get; set; }
        public double OriginValue { get; set; }
        public string RandomString { get; set; }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            var tests = new List<Test>()
            {
                new Test(){WMS_ID = 1,  OriginValue = 9 , RandomString = "dsaf1"},
                new Test(){WMS_ID = 2, OriginValue = 1 , RandomString = "dsaf2"},
                new Test(){WMS_ID = 1, OriginValue =3, RandomString = "dsaf3"},
                new Test(){WMS_ID  = null, OriginValue = 97, RandomString = "dsaf4"}
            };

            var transportItems = tests.AsQueryable();
            //This will group the table by wmsID and use the first row from each groups resulting in rows where  wmsID is distinct.
            var query = transportItems.GroupBy(x => x.WMS_ID)
                .Where(x => x.Key != null)
                .Select(x => x.FirstOrDefault());
            // execute query 
            var dict = query.ToDictionary(x => x.WMS_ID, x => x.OriginValue);
            foreach (var kvp in dict) Console.WriteLine($"{kvp.Key} => {kvp.Value}");
            Console.Read();
        }
    }
}
