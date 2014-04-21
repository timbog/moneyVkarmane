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
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Web;
using ClientLibrary;
namespace MoneyVkarmane
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MoneyVKarmaneClient client;

        private string temporaryLogin;

        public MainWindow()
        {
            client = new MoneyVKarmaneClient();
            InitializeComponent();
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
            try
            {
                bool flag = client.AddClient(newLoginBox.Text, newPasswordBox.Password, nameStr);
                if (flag)
                {
                    this.temporaryLogin = newLoginBox.Text;
                    registrationGrid.Visibility = System.Windows.Visibility.Hidden;
                    budgetTableGrid.Visibility = System.Windows.Visibility.Visible;
                    this.Height = 700;
                    this.Width = 1100;
                    budgetChangesDataGrid.ItemsSource = client.GetAllSums(newLoginBox.Text);
                    budgetChangesDataGrid.Columns[0].Header = "Дата и время";
                    budgetChangesDataGrid.Columns[1].Header = "Имя";
                    budgetChangesDataGrid.Columns[2].Header = "Сумма";
                    budgetChangesDataGrid.Columns[3].Header = "Валюта";
                    budgetChangesDataGrid.Columns[4].Header = "На что/Откуда";
                    budgetChangesDataGrid.Columns[5].Header = "Комментарий";
                    budgetChangesDataGrid.Columns[6].Header = "ID клиента";
                }
            }
            catch (System.ServiceModel.EndpointNotFoundException)
            {
                errorLabel.Content = "Сервер недоступен";
            }
        }

        private void addNewSumButton_Click(object sender, RoutedEventArgs e)
        {
            budgetChangesDataGrid.Opacity = 0.2;
            addNewSumBorder.Visibility = System.Windows.Visibility.Visible;
        }

        private void backNewSumButton_Click(object sender, RoutedEventArgs e)
        {
            addNewSumBorder.Visibility = System.Windows.Visibility.Hidden;
            budgetChangesDataGrid.Opacity = 1;
        }

        private void sumBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                if (double.Parse(sumBox.Text) > 0.0)
                {
                    addNewSumErrorLabel.Content = "";
                    aimOrWhereBox.Items.Clear();
                    aimOrWhereBox.Items.Add("Счет: вода");
                    aimOrWhereBox.Items.Add("Счет: газ");
                    aimOrWhereBox.Items.Add("Счет: электричество");
                    aimOrWhereBox.Items.Add("Счет: интернет");
                    aimOrWhereBox.Items.Add("Автомобиль");
                    aimOrWhereBox.Items.Add("Транспорт");
                    aimOrWhereBox.Items.Add("Кредит/ Долг");
                    aimOrWhereBox.Items.Add("Хобби/ Отдых");
                    aimOrWhereBox.Items.Add("Спорт");
                    aimOrWhereBox.Items.Add("Дом/ Дача");
                    aimOrWhereBox.Items.Add("Медикаменты");
                    aimOrWhereBox.Items.Add("Домашние животные");
                    aimOrWhereBox.Items.Add("Одежда");
                    aimOrWhereBox.Items.Add("Подарки");
                    aimOrWhereBox.Items.Add("Арендная плата");
                    aimOrWhereBox.Items.Add("Другое");
                }
                if (double.Parse(sumBox.Text) < 0.0)
                {
                    addNewSumErrorLabel.Content = "";
                    aimOrWhereBox.Items.Clear();
                    aimOrWhereBox.Items.Add("Зарплата");
                    aimOrWhereBox.Items.Add("Подарки");
                    aimOrWhereBox.Items.Add ("Долги");
                    aimOrWhereBox.Items.Add("Подработка");
                    aimOrWhereBox.Items.Add("Выигрыш");
                    aimOrWhereBox.Items.Add("Акции");
                    aimOrWhereBox.Items.Add("Арендная плата");
                    aimOrWhereBox.Items.Add("Другое");
                }
            }
            catch (FormatException)
            {
                    addNewSumErrorLabel.Content = "Ошибка";
            }
        }

        private void okAddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime data = new DateTime();
                if (nowCheckBox.IsChecked == true)
                    data = DateTime.Now;
                else
                    data = DateTime.Parse(timeBox.Text);
                string moneyType = "wwqa";
                if (rubleCheckBox.IsChecked == true)
                    moneyType = "руб";
                if (euroCheckBox.IsChecked == true)
                    moneyType = "€";
                if (dollarCheckBox.IsChecked == true)
                    moneyType = "$";               
                client.AddSum(temporaryLogin, nameBox.Text, double.Parse(sumBox.Text), aimOrWhereBox.Text, commentBox.Text, data, moneyType);
                budgetChangesDataGrid.ItemsSource = client.GetAllSums(temporaryLogin);
                addNewSumBorder.Visibility = System.Windows.Visibility.Hidden;
                budgetChangesDataGrid.Opacity = 1;

            }
            catch (Exception)
            {
            }
        }

        private void rubleCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            euroCheckBox.IsChecked = false;
            dollarCheckBox.IsChecked = false;
        }

        private void euroCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            rubleCheckBox.IsChecked = false;
            dollarCheckBox.IsChecked = false;
        }

        private void dollarCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            euroCheckBox.IsChecked = false;
            rubleCheckBox.IsChecked = false;
        }

        private void enterButton_Click(object sender, RoutedEventArgs e)
        {
            if (client.SuccesfulLogin(loginBox.Text, passwordBox.Password))
            {
                startGrid.Visibility = System.Windows.Visibility.Hidden;
                budgetTableGrid.Visibility = System.Windows.Visibility.Visible;
                temporaryLogin = loginBox.Text;
                budgetChangesDataGrid.ItemsSource = client.GetAllSums(temporaryLogin);
            }
        }

    }
}
