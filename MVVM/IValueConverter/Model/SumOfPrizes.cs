using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMdemo.Model
{
    class SumOfPrizes
    {
        public decimal Sum { get; private set; }
        public decimal Limit { get; private set;  }


        public SumOfPrizes(decimal Limit, decimal Sum=0)
        {
            this.Limit = Limit;
            this.Sum = Sum; 
        }
        public void Add(decimal price)
        {
            Sum += IsPriceCorrect(price) ? price : throw new ArgumentOutOfRangeException("Incorrect price.");
        }



        public bool IsPriceCorrect(decimal price)
        {
            bool isPositive = price >= 0;
            bool isInLimit = Sum + price <= Limit ;

            return isPositive && isInLimit;
        }


    }
}
