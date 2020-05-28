using System.Collections.Generic;
using System.Linq;
namespace PotterBooksBasket.logic
{
    public class PriceCalculator
    {
        public static double PriceForOneBookWithoutDiscount = 8.00;
        private readonly Discount[] _givenDiscount =
        {
            new Discount {NumberOfDifferentBooksOfASeries = 2, DiscountVariable = 0.95},
            new Discount {NumberOfDifferentBooksOfASeries = 3, DiscountVariable = 0.90},
            new Discount {NumberOfDifferentBooksOfASeries = 4, DiscountVariable = 0.80},
            new Discount {NumberOfDifferentBooksOfASeries = 5, DiscountVariable = 0.75}
        };


        //this is a solution for calculating the minimum price for a maximum of two series of books 
        public double CalculatePrice(List<Books> list)
        {
            double resultPrice = 0;
            var booksList = list.Where(w => w.Amount != 0).ToList();

            var numberOfAllBooks = booksList.Sum(w => w.Amount);

            if (numberOfAllBooks > 0)
            {
                var amountList = new List<int>();
                booksList.ForEach(b => amountList.Add(b.Amount));

                if (amountList.Max() == 2 && booksList.Count() > 1)
                {
                    var listOfPossiblePairs = GetListOfPossiblePairs(numberOfAllBooks);

                    var minResult = GetMinResult(listOfPossiblePairs);
                    return minResult;

                }
                {
                    while (booksList.Any())
                    {
                        var numberDistinctBooks = booksList.Count();

                        if (1 < numberDistinctBooks && numberDistinctBooks < 6)
                        {
                            var discount = GetDiscount(numberDistinctBooks);

                            resultPrice += numberDistinctBooks * PriceForOneBookWithoutDiscount * discount;
                        }
                        else
                        {
                            resultPrice += numberDistinctBooks * PriceForOneBookWithoutDiscount;
                        }

                        Reduction(booksList);
                    }
                    return resultPrice;
                }
            }

            return resultPrice;
        }

        private double GetMinResult(List<int[]> listOfPossiblePairs)
        {
            double resultPrice;
            var possiblePrices = new List<double>();
            foreach (var possiblePair in listOfPossiblePairs)
            {
                resultPrice = 0;

                foreach (var number in possiblePair)
                {
                    double discount = 1;
                    if (number > 1)
                    {
                        discount = GetDiscount(number);
                    }

                    resultPrice += number * PriceForOneBookWithoutDiscount * discount;
                }

                possiblePrices.Add(resultPrice);
            }

            var minResult = possiblePrices.Min();
            return minResult;
        }

        private double GetDiscount(int number)
        {
            var discount = _givenDiscount.Where(w => w.NumberOfDifferentBooksOfASeries == number)
                .SingleOrDefault().DiscountVariable;
            return discount;
        }

        // method to get the possible pairs for assembling two series with given number of all books
        private static List<int[]> GetListOfPossiblePairs(int numberOfAllBooks)
        {
            var listOfPossiblePairs = new List<int[]>();

            for (int i = 1; i < 6; i++)
            {
                var checkNumber = numberOfAllBooks - i;
                if (checkNumber > 0 && checkNumber < 6)
                {
                    var amountPackages = new int[2];
                    amountPackages[0] = i;
                    amountPackages[1] = checkNumber;
                    listOfPossiblePairs.Add(amountPackages);
                }
            }

            return listOfPossiblePairs;
        }

        // method to reduce the amount of the book type after its use and to delete the book type with amount 0
        private static void Reduction(List<Books> booksList)
        {
            booksList.ForEach(b => b.Amount -= 1);

            var removedItems = booksList.Where(b => b.Amount == 0).ToList();

            if (removedItems.Any())
            {
                removedItems.ForEach(r => { booksList.Remove(r); });
            }
        }
    }

}
