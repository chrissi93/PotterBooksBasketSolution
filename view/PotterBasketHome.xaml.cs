using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Castle.Core.Internal;
using PotterBooksBasket.logic;

namespace PotterBooksBasket.view
{
    /// <summary>
    /// interaction logic for PotterBasketHome.xaml
    /// </summary>
    public partial class PotterBasketHome : Page
    {
        private readonly PriceCalculator _priceCalculator;
        public PotterBasketHome()
        {
            _priceCalculator = new PriceCalculator();
            InitializeComponent();
        }

        private void Button_Click_Calculate(object sender, RoutedEventArgs e)
        {
            var bookList = new List<Books>();
            var valueList = new string[] {bookOne.Text, bookTwo.Text, bookThree.Text, bookFour.Text, bookFive.Text};

            for (int i = 0; i < 5; i++)
            {
                if (valueList[i].IsNullOrEmpty())
                {
                    valueList[i] = "0";
                }
            }
            bookList.Add(new Books { Name = logic.Name.Book1, Amount = Int32.Parse(valueList[0])});
            bookList.Add(new Books { Name = logic.Name.Book2, Amount = Int32.Parse(valueList[1]) });
            bookList.Add(new Books { Name = logic.Name.Book3, Amount = Int32.Parse(valueList[2]) });
            bookList.Add(new Books { Name = logic.Name.Book4, Amount = Int32.Parse(valueList[3]) });
            bookList.Add(new Books { Name = logic.Name.Book5, Amount = Int32.Parse(valueList[4])});

            result.Content = _priceCalculator.CalculatePrice(bookList).ToString();

        }

        // method for the WPF UI to ensure that the input type is integer 
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
