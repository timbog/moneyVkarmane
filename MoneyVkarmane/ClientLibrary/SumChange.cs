﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary
{
    public struct SumChange
    {
        public SumChange(string name, string aimOrWhere, Nullable<double> change, Nullable<System.DateTime> time, string comment, string money):this()
        {
            this.Name = name;
            this.Aim = aimOrWhere;
            this.Change = change;
            this.Time = time;
            this.Comment = comment;
            this.Money = money;
        }

        public Nullable<System.DateTime> Time { get; set; }

        public string Name { get; set; }

        public Nullable<double> Change { get; set; }

        public string Money { get; set; }

        public string Aim { get; set; }

        public string Comment { get; set; }

    }
}
