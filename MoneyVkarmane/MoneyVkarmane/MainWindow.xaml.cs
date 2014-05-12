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

        private Valutes valutes;

        public List<string> temporaryNameList;

        private bool allNameBoxesAreEmty()
        {
            return ((nameBox1.Text == "") && (nameBox2.Text == "") && (nameBox3.Text == "") && (nameBox4.Text == "")
                 && (nameBox5.Text == "") && (nameBox6.Text == "") && (nameBox7.Text == "") && (nameBox8.Text == "")
                 && (nameBox9.Text == ""));              
        }

        private void SetCourses()
        {
            try
            {
                valutes = new Valutes();
                var x = valutes.GetValuteCourse()[0];
                var y = valutes.GetValuteCourse()[1];
                this.dollarRoubleLabel.Content = "Курс долл.: " + x;
                this.euroRoubleLabel.Content = "Курс евро: " + y;
            }
            catch (System.Net.WebException)
            {
                this.euroRoubleLabel.Content = "Нет подключения";
                this.dollarRoubleLabel.Content = "к интернету";
            }
        }

        public MainWindow()
        {
            client = new MoneyVKarmaneClient();
            InitializeComponent();
            SetCourses();
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
                if ((this.confirmPassswordBox.Password != this.newPasswordBox.Password) || (this.newLoginBox.Text == "") ||
                    (this.newPasswordBox.Password == ""))
                {
                    registrationGrid.Opacity = 0.1;
                    errorCommentLabel.Content = "Неверные данные";
                    errorBorder.Visibility = System.Windows.Visibility.Visible;
                    return;
                }
                bool flag = client.AddClient(newLoginBox.Text, newPasswordBox.Password, nameStr);
                if (flag)
                {
                    this.temporaryLogin = newLoginBox.Text;
                    this.temporaryNameList = client.GetNameList(temporaryLogin);
                    this.temporaryClientButton.Content = this.temporaryLogin;
                    registrationGrid.Visibility = System.Windows.Visibility.Hidden;
                    budgetTableGrid.Visibility = System.Windows.Visibility.Visible;
                    this.Height = 700;
                    this.Width = 1150;
                    budgetChangesDataGrid.ItemsSource = client.GetAllSums(newLoginBox.Text);
                    for (int i = 0; i < this.temporaryNameList.Count; ++i)
                    {
                        if (nameTextBlock.Text != "")
                            nameTextBlock.Text = nameTextBlock.Text + ", " + this.temporaryNameList[i];
                        else
                            nameTextBlock.Text = this.temporaryNameList[i];
                    }
                    this.roubleLabel.Content = "Руб.: " + client.GetNowBudget(temporaryLogin)[0].ToString();
                    this.euroLabel.Content = "€: " + client.GetNowBudget(temporaryLogin)[1].ToString();
                    this.dollarLabel.Content = "$: " + client.GetNowBudget(temporaryLogin)[2].ToString();
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
            statisticsDataGrid.Opacity = 0.1;

            addNewSumBorder.Visibility = System.Windows.Visibility.Visible;
            addNewNameBorder.Focus();
            nameComboBox.Items.Clear();
            for (int i = 0; i < temporaryNameList.Count; ++i)
            {
                nameComboBox.Items.Add(temporaryNameList[i]);
            }
        }

        private void backNewSumButton_Click(object sender, RoutedEventArgs e)
        {
            addNewSumBorder.Visibility = System.Windows.Visibility.Hidden;
            budgetChangesDataGrid.Opacity = 1;
            statisticsDataGrid.Opacity = 1;
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
                statisticsDataGrid.Opacity = 1;

                commentBox.Text = "";
                sumBox.Text = "";
                euroCheckBox.IsChecked = false;
                dollarCheckBox.IsChecked = false;
                rubleCheckBox.IsChecked = false;
                this.roubleLabel.Content = "Руб.: " + client.GetNowBudget(temporaryLogin)[0].ToString();
                this.euroLabel.Content = "€: " + client.GetNowBudget(temporaryLogin)[1].ToString();
                this.dollarLabel.Content = "$: " + client.GetNowBudget(temporaryLogin)[2].ToString();
                this.timeBox.Text = "";
                this.nowCheckBox.IsChecked = false;
            }
            catch (System.FormatException)
            {
                budgetTableGrid.Opacity = 0.1;
                statisticsDataGrid.Opacity = 0.1;
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
                    this.Width = 1150;
                    startGrid.Visibility = System.Windows.Visibility.Hidden;
                    budgetTableGrid.Visibility = System.Windows.Visibility.Visible;
                    temporaryLogin = loginBox.Text;
                    this.temporaryClientButton.Content = this.temporaryLogin;
                    temporaryNameList = client.GetNameList(temporaryLogin);
                    budgetChangesDataGrid.ItemsSource = client.GetAllSums(this.temporaryLogin);
                    for (int i = 0; i < this.temporaryNameList.Count; ++i)
                    {
                        if (nameTextBlock.Text != "")
                            nameTextBlock.Text = nameTextBlock.Text + ", " + this.temporaryNameList[i];
                        else
                            nameTextBlock.Text = this.temporaryNameList[i];
                    }
                    this.roubleLabel.Content = "Руб.: " + client.GetNowBudget(temporaryLogin)[0].ToString();
                    this.euroLabel.Content = "€: " + client.GetNowBudget(temporaryLogin)[1].ToString();
                    this.dollarLabel.Content = "$: " + client.GetNowBudget(temporaryLogin)[2].ToString();
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
                    errorCommentLabel.Content = "Нет подключения к серверу";
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
            this.nameTextBlock.Text = "";
            this.confirmPassswordBox.Password = "";
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
            this.roubleLabel.Content = "Руб.: " + client.GetNowBudget(temporaryLogin)[0].ToString();
            this.euroLabel.Content = "€: " + client.GetNowBudget(temporaryLogin)[1].ToString();
            this.dollarLabel.Content = "$: " + client.GetNowBudget(temporaryLogin)[2].ToString();
        }

        private void budgetChangesDataGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            budgetChangesDataGrid.UnselectAll();
            deleteRowButton.Visibility = System.Windows.Visibility.Hidden;
        }

        private void addNameButton_Click(object sender, RoutedEventArgs e)
        {
            budgetChangesDataGrid.Opacity = 0.1;
            statisticsDataGrid.Opacity = 0.1;
            addNewNameBorder.Visibility = System.Windows.Visibility.Visible;
        }

        private void addNewNameBackButton_Click(object sender, RoutedEventArgs e)
        {
            budgetChangesDataGrid.Opacity = 1;
            statisticsDataGrid.Opacity = 1;
            addNewNameBorder.Visibility = System.Windows.Visibility.Hidden;
        }

        private void addNewNameOkButton_Click(object sender, RoutedEventArgs e)
        {
            if (newNameTextBox.Text != "")
            {
                client.AddNewName(this.temporaryLogin, newNameTextBox.Text);
                nameTextBlock.Text = nameTextBlock.Text + ", " + newNameTextBox.Text;
                this.temporaryNameList.Add(newNameTextBox.Text);
                
            }
            budgetChangesDataGrid.Opacity = 1;
            statisticsDataGrid.Opacity = 1;
            addNewNameBorder.Visibility = System.Windows.Visibility.Hidden;            
        }

        private void tableButton_Click(object sender, RoutedEventArgs e)
        {
            statisticsDataGrid.Visibility = System.Windows.Visibility.Hidden;
            budgetChangesDataGrid.Visibility = System.Windows.Visibility.Visible;
            this.dataGridLabel.Content = "Таблица данных";
            addNewSumButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void statisticsButton_Click(object sender, RoutedEventArgs e)
        {
            budgetChangesDataGrid.Visibility = System.Windows.Visibility.Hidden;
            statisticsDataGrid.Visibility = System.Windows.Visibility.Visible;
            this.dataGridLabel.Content = "Статистика";
            statisticsDataGrid.ItemsSource = client.GetStat(this.temporaryLogin, valutes.GetValuteCourse()[0], valutes.GetValuteCourse()[1]);
            statisticsDataGrid.Items.Refresh();
            statisticsDataGrid.Columns[0].IsReadOnly = true;
            statisticsDataGrid.Columns[1].IsReadOnly = true;
            addNewSumButton.Visibility = System.Windows.Visibility.Hidden;
            deleteRowButton.Visibility = System.Windows.Visibility.Hidden;
        }

        private void temporaryClientButton_Click(object sender, RoutedEventArgs e)
        {
            budgetChangesDataGrid.Opacity = 0.1;
            statisticsDataGrid.Opacity = 0.1;
            changePasswordBorder.Visibility = System.Windows.Visibility.Visible;
            addNewSumButton.IsEnabled = false;
        }

        private void okChangeButton_Click(object sender, RoutedEventArgs e)
        {

            if (changedPasswordBox.Password != confirmChangePasswordBox.Password)
            {
                errorBorder.Visibility = System.Windows.Visibility.Visible;
                budgetTableGrid.Opacity = 0.1;
                errorCommentLabel.Content = "Введите верные данные";
            }
            else
            {
                if (client.AnotherPassword(this.temporaryLogin, oldPasswordBox.Password, changedPasswordBox.Password) == false)
                {
                    errorBorder.Visibility = System.Windows.Visibility.Visible;
                    budgetTableGrid.Opacity = 0.1;
                    errorCommentLabel.Content = "Введите верные данные";
                }
                else
                {
                    exitPasswords();
                }
            }
        }

        private void exitPasswords()
        {
            changePasswordBorder.Visibility = System.Windows.Visibility.Hidden;
            budgetChangesDataGrid.Opacity = 1;
            statisticsDataGrid.Opacity = 1;
            oldPasswordBox.Password = "";
            changedPasswordBox.Password = "";
            confirmChangePasswordBox.Password = "";
            addNewSumButton.IsEnabled = true;
        }

        private void backChangeButton_Click(object sender, RoutedEventArgs e)
        {
            exitPasswords();
        }
       
    }
}
