using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MoneyVkarmane
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //mainGrid.Visibility = System.Windows.Visibility.Hidden;
        }

        private void registrationButton_Click(object sender, RoutedEventArgs e)
        {
            startGrid.Visibility = System.Windows.Visibility.Hidden;
            registrationGrid.Visibility = System.Windows.Visibility.Visible;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            registrationGrid.Visibility = System.Windows.Visibility.Hidden;
            startGrid.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
