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
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool AddNewClient(string login, string password, string names);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        void AddNewSum(string login, string newName, double sum, string aim, string comment, DateTime date, string monType);

        [OperationContract]
        [XmlSerializerFormat]
        [WebGet(RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        List<SumChange> GetSums(string login);
    }
}
