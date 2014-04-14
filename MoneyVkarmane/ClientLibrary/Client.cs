using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Web;

namespace ClientLibrary
{
    public class Client
    {
        public IService service { get; private set; }

        ChannelFactory<IService> cf;

        public Client(string str)
        {
            cf = new ChannelFactory<IService>(new WebHttpBinding(), str);
            cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
            service = cf.CreateChannel();
        }
    }
}
