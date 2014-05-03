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
using System.Data;

namespace MoneyVkarmane
{
    class Valutes
    {
        public RBKServise.DailyInfo di{get;set;}

        public Valutes()
        {
            this.di = new RBKServise.DailyInfo();
        }

        public double[] GetValuteCourse()
        {
            DataSet dsc = (System.Data.DataSet)di.GetCursOnDate(DateTime.Now);
            var dolCourse = double.Parse(dsc.Tables[0].Rows[9][2].ToString());
            var euroCourse = double.Parse(dsc.Tables[0].Rows[10][2].ToString());
            double[] array = new double[2];
            array[0] = dolCourse;
            array[1] = euroCourse;
            return array;
        }
    }
}
