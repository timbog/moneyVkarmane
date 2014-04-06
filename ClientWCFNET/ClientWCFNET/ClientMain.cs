using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Web;
namespace ClientWCFNET
{
    class ClientMain
    {
        static Client client;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter IP-adress: ");
            client = new Client(Console.ReadLine());
        }
    }
}
