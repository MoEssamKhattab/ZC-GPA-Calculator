using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZC_GPA_Calculator
{
    internal class Core
    {
        Dictionary<string, double> grades = new Dictionary<string, double>(){
            {"A", 4},
            {"A-", 3.7},
            {"B+", 3.3},
            {"B", 3},
            {"B-", 2.7},
            {"C+", 2.3},
            {"C", 2},
            {"C-", 1.7},
            {"F", 0}
        };

    }
}
