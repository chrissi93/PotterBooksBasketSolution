using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterBooksBasket.logic
{
    public class Books
    {
        public Name Name { get; set; }

        public int Amount { get; set; }

        public void AssignValues(Name bName, int amount)
        {
            Name = bName;
            Amount = amount;
        }
    }

    public enum Name
    {
        Book1,
        Book2,
        Book3,
        Book4,
        Book5
    }


}
