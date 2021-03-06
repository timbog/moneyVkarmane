﻿using System;
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

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool CorrectEnter(string login, string password);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        int GetId(string login);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetNames(string login);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        void DeleteItem(string login, string name, Nullable<double> sum, string aim, string comment, Nullable<System.DateTime> date, string monType);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        void AddName(string login, string name);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        Nullable<double>[] GetBudget(string login);

        [OperationContract]
        [XmlSerializerFormat]
        [WebGet(RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Statistics> Stat(string login, double course1, double course2);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool ChangePassword(string login, string oldPassword, string newPassword);
    }
}
