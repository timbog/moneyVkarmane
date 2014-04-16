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
using ClientLibrary;
namespace MoneyVkarmane
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MoneyVKarmaneClient client;

        public MainWindow()
        {
            client = new MoneyVKarmaneClient();
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

        private void addMembersButton_Click(object sender, RoutedEventArgs e)
        {
            registrationGrid.Visibility = System.Windows.Visibility.Hidden;
            nameGrid.Visibility = System.Windows.Visibility.Visible;
        }

        private void okNameButton_Click(object sender, RoutedEventArgs e)
        {
            nameGrid.Visibility = System.Windows.Visibility.Hidden;
            registrationGrid.Visibility = System.Windows.Visibility.Visible;
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            string nameStr = nameBox1.Text + "," + nameBox2.Text + "," + nameBox3.Text + "," + nameBox4.Text + "," + nameBox5.Text + "," + nameBox6.Text + "," + nameBox7.Text + "," + nameBox8.Text + "," + nameBox9.Text;
                client.AddClient(newLoginBox.Text, newPasswordBox.Password, nameStr);
        }
    }
}
