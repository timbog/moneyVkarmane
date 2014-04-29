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

        public List<string> temporaryNameList;

        private bool allNameBoxesAreEmty()
        {
            return ((nameBox1.Text == "") && (nameBox2.Text == "") && (nameBox3.Text == "") && (nameBox4.Text == "")
                 && (nameBox5.Text == "") && (nameBox6.Text == "") && (nameBox7.Text == "") && (nameBox8.Text == "")
                 && (nameBox9.Text == ""));              
        }

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
                if (this.allNameBoxesAreEmty())
                {
                    registrationGrid.Opacity = 0.1;
                    errorCommentLabel.Content = "Укажите хотя бы одного члена семьи";
                    errorBorder.Visibility = System.Windows.Visibility.Visible;
                    return;
                }
                bool flag = client.AddClient(newLoginBox.Text, newPasswordBox.Password, nameStr);
                if (flag)
                {
                    this.temporaryLogin = newLoginBox.Text;
                    this.temporaryNameList = client.GetNameList(temporaryLogin);
                    registrationGrid.Visibility = System.Windows.Visibility.Hidden;
                    budgetTableGrid.Visibility = System.Windows.Visibility.Visible;
                    this.Height = 700;
                    this.Width = 1100;
                    budgetChangesDataGrid.ItemsSource = client.GetAllSums(newLoginBox.Text);
                }
                else
                {
                    registrationGrid.Opacity = 0.1;
                    errorCommentLabel.Content = "Данный логин уже занят";
                    errorBorder.Visibility = System.Windows.Visibility.Visible;
                }
            }
            catch (System.ServiceModel.EndpointNotFoundException)
            {
                registrationGrid.Opacity = 0.1;
                errorCommentLabel.Content = "Нет подключения к серверу";
                errorBorder.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void addNewSumButton_Click(object sender, RoutedEventArgs e)
        {
            budgetChangesDataGrid.Opacity = 0.1;
            addNewSumBorder.Visibility = System.Windows.Visibility.Visible;
            nameComboBox.ItemsSource = this.temporaryNameList;
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
                if (double.Parse(sumBox.Text) < 0.0)
                {
                    addNewSumErrorLabel.Content = "";
                    aimOrWhereBox.Items.Clear();
                    aimOrWhereBox.Items.Add("Счет: вода");
                    aimOrWhereBox.Items.Add("Счет: газ");
                    aimOrWhereBox.Items.Add("Счет: электричество");
                    aimOrWhereBox.Items.Add("Счет: интернет");
                    aimOrWhereBox.Items.Add("Питание");
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
                if (double.Parse(sumBox.Text) > 0.0)
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
                client.AddSum(temporaryLogin, nameComboBox.Text, double.Parse(sumBox.Text), aimOrWhereBox.Text, commentBox.Text, data, moneyType);
                budgetChangesDataGrid.ItemsSource = null;
                budgetChangesDataGrid.ItemsSource = client.GetAllSums(this.temporaryLogin);
                addNewSumBorder.Visibility = System.Windows.Visibility.Hidden;
                budgetChangesDataGrid.Opacity = 1;

            }
            catch (System.FormatException)
            {
                budgetTableGrid.Opacity = 0.1;
                errorCommentLabel.Content = "Укажите верные данные";
                errorBorder.Visibility = System.Windows.Visibility.Visible;
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
            try
            {
                if (client.SuccesfulLogin(loginBox.Text, passwordBox.Password))
                {
                    this.Height = 700;
                    this.Width = 1100;
                    startGrid.Visibility = System.Windows.Visibility.Hidden;
                    budgetTableGrid.Visibility = System.Windows.Visibility.Visible;
                    temporaryLogin = loginBox.Text;
                    temporaryNameList = client.GetNameList(temporaryLogin);
                    budgetChangesDataGrid.ItemsSource = client.GetAllSums(this.temporaryLogin);
                }
                else
                {
                    startGrid.Opacity = 0.1;
                    errorBorder.Visibility = System.Windows.Visibility.Visible;
                    errorCommentLabel.Content = "Неверный логин/пароль";
                }
            }
            catch (System.ServiceModel.EndpointNotFoundException)
                {
                    startGrid.Opacity = 0.1;
                    errorBorder.Visibility = System.Windows.Visibility.Visible;
                }
        }

        private void errorOkButton_Click(object sender, RoutedEventArgs e)
        {
            errorBorder.Visibility = System.Windows.Visibility.Hidden;
            startGrid.Opacity = 1;
            registrationGrid.Opacity = 1;
            budgetTableGrid.Opacity = 1;
        }

        private void budgetChangesDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //cancel the auto generated column
            e.Cancel = true;

            //Get the existing column
            DataGridTextColumn dgTextC = (DataGridTextColumn)e.Column;

            //Create a new template column 
            DataGridTemplateColumn dgtc = new DataGridTemplateColumn();

            DataTemplate dataTemplate = new DataTemplate(typeof(DataGridCell));

            FrameworkElementFactory tb = new FrameworkElementFactory(typeof(TextBlock));
            tb.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
            dataTemplate.VisualTree = tb;
            dgtc.Header = dgTextC.Header;
            dgtc.CellTemplate = dataTemplate;          
            string headername = dgtc.Header.ToString();
            tb.SetBinding(TextBlock.TextProperty, dgTextC.Binding);
            if (headername == "Time")
                dgtc.Header = "Время";
            if (headername == "Name")
                dgtc.Header = "Имя";
            if (headername == "Change")
                dgtc.Header = "Сумма";
            if (headername == "Money")
                dgtc.Header = "Валюта";
            if (headername == "Aim")
                dgtc.Header = "Детали";
            if (headername == "Comment")
                dgtc.Header = "Коммент.";
            //add column back to data grid
            DataGrid dg = sender as DataGrid;
            if (dg.Columns.Count() <= 5)
                dg.Columns.Add(dgtc);
        }

        private void nowCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            timeBox.Text = DateTime.Now.ToString();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            budgetTableGrid.Visibility = System.Windows.Visibility.Hidden;
            startGrid.Visibility = System.Windows.Visibility.Visible;
            this.Height = 550;
            this.Width = 900;
            this.temporaryLogin = "";
            this.temporaryNameList.Clear();
            loginBox.Text = "";
            passwordBox.Password = "";
            newLoginBox.Text = "";
            newPasswordBox.Password = "";
            sumBox.Text = "";
            commentBox.Text = "";
            this.nameBox1.Text = "";
            this.nameBox2.Text = "";
            this.nameBox3.Text = "";
            this.nameBox4.Text = "";
            this.nameBox5.Text = "";
            this.nameBox6.Text = "";
            this.nameBox7.Text = "";
            this.nameBox8.Text = "";
            this.nameBox9.Text = "";
        }

        private void budgetChangesDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
                deleteRowButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void deleteRowButton_Click(object sender, RoutedEventArgs e)
        {
            SumChange change = (SumChange)budgetChangesDataGrid.SelectedItem;
            client.DeleteRecord(this.temporaryLogin, change.Name, change.Change, change.Aim,change.Comment, change.Time, change.Money);
            budgetChangesDataGrid.ItemsSource = client.GetAllSums(this.temporaryLogin);
            deleteRowButton.Visibility = System.Windows.Visibility.Hidden;
        }

        private void budgetChangesDataGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            budgetChangesDataGrid.UnselectAll();
            deleteRowButton.Visibility = System.Windows.Visibility.Hidden;
        }

    }
}
