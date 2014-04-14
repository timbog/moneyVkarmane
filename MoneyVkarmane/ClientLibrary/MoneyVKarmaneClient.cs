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
            this.client = new Client("http://192.168.0.108:8000");
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

        public List<string> GetAllSums(string login)
        {
            return client.service.GetSums(login);
        }
    }
}
