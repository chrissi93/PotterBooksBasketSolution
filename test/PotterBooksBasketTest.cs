using System;
using System.Collections.Generic;
using PotterBooksBasket.logic;
using Xunit;

namespace PotterBooksBasket.test
{

    public class PotterBooksBasketTest
    {
        private readonly PriceCalculator _priceCalculator;
        public Name[] EnumValues;

        public PotterBooksBasketTest()
        {
            _priceCalculator = new PriceCalculator();
            EnumValues = (Name[])Enum.GetValues(typeof(Name));


        }

        [Fact]
        public void ZeroBookInBasket()
        {
            var booksList = new List<Books>();
            var price = _priceCalculator.CalculatePrice(booksList);
            Assert.Equal(0, price);
        }

        [Fact]
        public void OneBookInBasket()
        {

            foreach (var bookName in EnumValues)
            {
                var book = new Books();
                var booksList = new List<Books>();
                book.AssignValues(bookName, 1);
                booksList.Add(book);
                var price = _priceCalculator.CalculatePrice(booksList);
                Assert.Equal(8, price);
            }
        }

        [Fact]
        public void TwoIdenticalBooksInBasket()
        {
            CheckIdenticalBooks(Name.Book1, 2, 16);
        }

        [Fact]
        public void ThreeIdenticalBooksInBasket()
        {
            CheckIdenticalBooks(Name.Book2, 3, 24);

        }
        [Fact]
        public void FourIdenticalBooksInBasket()
        {
            CheckIdenticalBooks(Name.Book3, 4, 32);

        }

        [Fact]
        public void FiveIdenticalBooksInBasket()
        {
            CheckIdenticalBooks(Name.Book4, 5, 40);

        }

        [Fact]
        public void TwoDifferentBooksInBasket()
        {
            CheckPriceOfDifferentBooksWithGivenAmount(2, 15.2, 1);
        }


        [Fact]
        public void ThreeDifferentBooksInBasket()
        {
            CheckPriceOfDifferentBooksWithGivenAmount(3, 21.6, 1);

        }

        [Fact]
        public void FourDifferentBooksInBasket()
        {
            CheckPriceOfDifferentBooksWithGivenAmount(4, 25.6, 1);

        }

        [Fact]
        public void FiveDifferentBooksInBasket()
        {
            CheckPriceOfDifferentBooksWithGivenAmount(5, 30, 1);
        }

        [Fact]
        public void TwoTimesTwoDifferentBooksInBasket()
        {
            CheckPriceOfDifferentBooksWithGivenAmount(2, 29.6, 2);
        }

        [Fact]
        public void FourTimesTwoDifferentBooksInBasket()
        {
            CheckPriceOfDifferentBooksWithGivenAmount(4, 51.2, 2);
        }

        [Fact]
        public void TwoDifferentBooksAndSingleBookInBasket()
        {
            CheckPriceOfTwoDifferentBooks(2, 1, 23.2);
        }

        [Fact]
        public void TwoDifferentBooks_3_1InBasket()
        {

            CheckPriceOfTwoDifferentBooks(3, 1, 31.2);
        }

        [Fact]
        public void TwoDifferentBooks_2_3InBasket()
        {

            CheckPriceOfTwoDifferentBooks(3, 2, 38.4);
        }

        [Fact]
        public void GivenExample()
        {
            var booksList = new List<Books>();
            var bookOne = new Books();
            var bookTwo = new Books();
            var bookThree = new Books();
            var bookFour = new Books();
            var bookFive = new Books();
            bookOne.AssignValues(Name.Book1, 2);
            bookTwo.AssignValues(Name.Book2, 2);
            bookThree.AssignValues(Name.Book3, 2);
            bookFour.AssignValues(Name.Book4, 1);
            bookFive.AssignValues(Name.Book5, 1);
            booksList.Add(bookOne);
            booksList.Add(bookTwo);
            booksList.Add(bookThree);
            booksList.Add(bookFour);
            booksList.Add(bookFive);

            var price = _priceCalculator.CalculatePrice(booksList);
            Assert.Equal(51.2, price);
        }


        private void CheckIdenticalBooks(Name bookName, int bookAmount, double expectedResult)
        {
            var booksList = new List<Books>();
            var book = new Books();

            book.AssignValues(bookName, bookAmount);

            booksList.Add(book);

            var price = _priceCalculator.CalculatePrice(booksList);
            Assert.Equal(expectedResult, price);
        }

        private void CheckPriceOfTwoDifferentBooks(int amountBookOne, int amountBookTwo, double expectedPrice)
        {
            var booksList = new List<Books>();
            var bookOne = new Books();
            var bookTwo = new Books();
            bookOne.AssignValues(Name.Book1, amountBookOne);
            bookTwo.AssignValues(Name.Book2, amountBookTwo);
            booksList.Add(bookOne);
            booksList.Add(bookTwo);

            var price = _priceCalculator.CalculatePrice(booksList);
            Assert.Equal(expectedPrice, price);
        }

        private void CheckPriceOfDifferentBooksWithGivenAmount(int differentBookAmount, double expectedPrice, int amountOfBooks)
        {
            var booksList = new List<Books>();
            for (int i = 0; i < differentBookAmount; i++)
            {
                var book = new Books();
                book.AssignValues(EnumValues[i], amountOfBooks);
                booksList.Add(book);
            }

            var price = _priceCalculator.CalculatePrice(booksList);
            Assert.Equal(expectedPrice, price);
        }


    }
}
