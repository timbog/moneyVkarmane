using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary
{
    public class MoneyVKarmaneClient
    {
        public MoneyVKarmaneClient()
        {
            this.client = new Client("http://77.241.39.168:8000/");
        }

        private Client client;
        
        public bool AddClient(string login, string password, string names)
        {
            return client.service.AddNewClient(login, password, names);
        }

        public void AddSum(string login, string newName, double sum, string aim, string newComment, DateTime date, string monType)
        {
            client.service.AddNewSum(login, newName, sum, aim, newComment, date, monType);
        }

        public List<SumChange> GetAllSums(string login)
        {
            return client.service.GetSums(login);
        }

        public bool SuccesfulLogin(string login, string password)
        {
            return client.service.CorrectEnter(login, password);
        }

        public int GetIdentification(string login)
        {
            return client.service.GetId(login);
        }

        public List<string> GetNameList(string login)
        {
            string temp = client.service.GetNames(login);
            int iter = 0;
            List<string> nameList = new List<string>();
            string name = "";
            while (iter < temp.Length)
            {
                if (temp[iter] != ',')
                {
                    name = name + temp[iter];
                    if (iter == temp.Length - 1) 
                        nameList.Add(name);   
                }                
                else
                {
                    if (!String.Equals(name, ""))
                        nameList.Add(name); 
                    name = "";       
                }
                ++iter;
            }
            return nameList;
        }

        public void DeleteRecord(string login, string name, Nullable<double> sum, string aim, string comment, Nullable<System.DateTime> date, string monType)
        {
            client.service.DeleteItem(login, name, sum, aim, comment, date, monType);
        }

        public void AddNewName(string login, string name)
        {
            client.service.AddName(login, name);
        }

        public Nullable<double>[] GetNowBudget(string login)
        {
            return client.service.GetBudget(login);
        }

        public List<Statistics> GetStat(string login, double course1, double course2)
        {
            return client.service.Stat(login,course1,course2);
        }

        public bool AnotherPassword(string login, string oldPassword, string newPassword)
        {
            return client.service.ChangePassword(login, oldPassword, newPassword);
        }
    }
}
