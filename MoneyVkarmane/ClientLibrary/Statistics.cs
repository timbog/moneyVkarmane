using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary
{
    public struct Statistics
    {
        public string stat { get; set; }

        public string sum { get; set; }

        public Statistics(string nameOfstat, string sum)
            : this() 
        {
            this.stat = nameOfstat;
            this.sum = sum;
        }
    }
}
